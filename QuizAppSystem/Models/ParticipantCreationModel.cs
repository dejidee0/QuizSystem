// ParticipantCreationModel.cs
using System;

namespace QuizAppSystem.Models
{
    public class ParticipantCreationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string PasswordHash { get; set; }
        public string PIN { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsCompleted { get; set; }
        // Add any other properties specific to participant creation
    }
}
