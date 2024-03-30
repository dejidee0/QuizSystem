using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.DTO;
using QuizAppSystem.Service.Interface;
using QuizAppSystem.Common;

namespace QuizAppSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        }

        [HttpGet("{walletId}")]
        public async Task<ActionResult<ApiResponse<WalletDTO>>> GetWallet(string walletId)
        {
            var response = new ApiResponse<WalletDTO>();

            try
            {
                var wallet = await _walletService.GetWalletByIdAsync(walletId);
                if (wallet != null)
                {
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.Data = wallet;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Wallet not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<WalletDTO>>> CreateWallet([FromBody] WalletDTO walletDTO)
        {
            var response = new ApiResponse<WalletDTO>();

            try
            {
                await _walletService.CreateWalletAsync(walletDTO);
                response.StatusCode = 201;
                response.Message = "Wallet created successfully";
                response.Data = walletDTO;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpPut("{walletId}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateWallet(string walletId, [FromBody] WalletDTO updatedWalletDTO)
        {
            var response = new ApiResponse<string>();

            try
            {
                await _walletService.UpdateWalletAsync(walletId, updatedWalletDTO);
                response.StatusCode = 200;
                response.Message = "Wallet updated successfully";
                response.Data = "Wallet updated successfully.";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpDelete("{walletId}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteWallet(string walletId)
        {
            var response = new ApiResponse<string>();

            try
            {
                await _walletService.DeleteWalletAsync(walletId);
                response.StatusCode = 200;
                response.Message = "Wallet deleted successfully";
                response.Data = "Wallet deleted successfully.";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }
    }
}
