using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QuizAppSystem.Models;

namespace QuizAppSystem.Service.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, List<string> carbonCopy = null);
    }
}



