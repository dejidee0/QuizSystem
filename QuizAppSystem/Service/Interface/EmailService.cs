using System.Text.Json;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using QuizAppSystem.Models;

namespace QuizAppSystem.Service.Interface
{
    public class EmailService : IEmailSender
    {
        private readonly HttpClient _httpClient;
        private readonly string _emailServiceUrl;

        public EmailService(HttpClient httpClient, string emailServiceUrl)
        {
            _httpClient = httpClient;
            _emailServiceUrl = emailServiceUrl;
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

                var jsonRequest = JsonSerializer.Serialize(emailRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_emailServiceUrl, content);

                
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}