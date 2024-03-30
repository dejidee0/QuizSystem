// Participant.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace QuizAppSystem.Models
{
    public class Participant
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsCompleted { get; set; }

        
        public string PIN { get; set; }
    }
}
