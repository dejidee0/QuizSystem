using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizAppSystem.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtToken { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
        public string? ConfirmationToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public string? EmailConfirmationToken { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
        //public string ResetPasswordToken { get; set; }
        public ICollection<PaymentTransaction> Transactions { get; set; }
        public ICollection<Exam> CreatedExams { get; set; }

    }
}
