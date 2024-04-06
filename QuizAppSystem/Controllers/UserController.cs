using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QuizAppSystem.Models;
using QuizAppSystem.Service.Interface;
using QuizAppSystem.ViewModels;

namespace QuizAppSystem.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<UserController> logger,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Set the correct property consistently
                    user.EmailConfirmationToken = WebUtility.UrlDecode(token);
                    await _userManager.UpdateAsync(user);

                    // Assign role based on user input
                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Generate JWT token
                    var jwtToken = CreateToken(user);

                    // Save the token to the database
                    user.JwtToken = jwtToken;
                    await _userManager.UpdateAsync(user);

                    // Send confirmation email using EmailService
                    var confirmationLink = Url.Action("ConfirmEmail", "User", new { userId = user.Id, token }, Request.Scheme);
                    var emailBody = $"Hello!! User Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";

                    // Use the EmailService to send the email
                    var emailRequest = new EmailRequest
                    {
                        To = user.Email,
                        Subject = "Confirm your email",
                        Body = emailBody
                    };

                    var emailResult = await _emailSender.SendEmailAsync(emailRequest.To, emailRequest.Subject, emailRequest.Body, emailRequest.CarbonCopy);

                    if (emailResult)
                    {
                        return Ok(new { Message = "Nice! User registered. Confirmation email sent Gracias!.", Token = jwtToken });
                    }
                    else
                    {
                        return BadRequest("Failed to send confirmation email.");
                    }
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    // Generate and return JWT token
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var token = CreateToken(user);

                    return Ok(new { token });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return StatusCode(403, "Account locked out.");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return BadRequest(ModelState);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
     _configuration.GetSection("Authentication:ApiSettings:Secret").Value));



            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

            //private string GenerateJwtToken(User user)
            //{
            //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //    var claims = new[]
            //    {
            //        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //        new Claim(ClaimTypes.NameIdentifier, user.Id)
            //    };

            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["JwtSettings:Issuer"],
            //        audience: _configuration["JwtSettings:Audience"],
            //        claims: claims,
            //        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
            //        signingCredentials: credentials
            //    );

            //    return new JwtSecurityTokenHandler().WriteToken(token);
        }





        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate password reset token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Save the token to the database (optional, depends on your requirements)
                    user.PasswordResetToken = token;
                    await _userManager.UpdateAsync(user);

                    // Send reset password email
                    var emailBody = $"Please reset your password by clicking <a href='{Request.Scheme}://{Request.Host}/reset-password?email={model.Email}&token={WebUtility.UrlEncode(token)}'>here</a>.";
                    await _emailSender.SendEmailAsync(user.Email, "Reset your password", emailBody);

                    _logger.LogInformation($"Forgot password request for user: {model.Email}. Reset token: {token}");

                    return Ok("Password reset email sent.");
                }

                // Don't reveal that the user does not exist or is not confirmed
                return Ok("Password reset email sent (if the email exists and is confirmed).");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Generate password reset token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Save the token to the database
                    user.PasswordResetToken = token;
                    await _userManager.UpdateAsync(user);

                    // Reset the password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                    if (result.Succeeded)
                    {
                        // Clear the reset password token after successful reset
                        user.PasswordResetToken = null;
                        await _userManager.UpdateAsync(user);

                        _logger.LogInformation($"Password reset for user: {model.Email}");
                        return Ok("Password reset successful.");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                // Don't reveal that the user does not exist
                return Ok("Password reset not successful (if the email exists).");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Invalid confirmation link.");
            }

            // URL decode the token to match the encoding in the database
            var decodedToken = WebUtility.UrlDecode(token);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // ConfirmEmailAsync expects the original, not URL-decoded token
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            return result.Succeeded ? Ok("Email confirmed successfully.") : BadRequest("Email confirmation failed.");
        }
    }
}
