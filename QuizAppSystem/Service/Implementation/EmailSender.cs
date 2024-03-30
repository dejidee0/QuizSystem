using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QuizAppSystem.Models;
using QuizAppSystem.Service.Interface;

namespace QuizAppSystem.Service.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly HttpClient _httpClient;
        private readonly string _emailServiceUrl;

        public EmailSender(HttpClient httpClient, IOptions<EmailSettings> emailSettings)
        {
            _httpClient = httpClient;
            _emailServiceUrl = emailSettings.Value.EmailServiceUrl;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, List<string> carbonCopy = null)
        {
            try
            {
                var emailRequest = new
                {
                    to,
                    subject,
                    body,
                    carbonCopy
                };

                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(emailRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_emailServiceUrl, content);

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}