using System;

namespace QuizAppSystem.DTO
{
    public class ParticipantDTO
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PIN { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}


