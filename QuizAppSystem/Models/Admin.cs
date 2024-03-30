namespace QuizAppSystem.Models
{
    public class Admin : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        public DateTime RegistrationDate { get; set; }

        public List<Exam> CreatedExams { get; set; } = new List<Exam>(); 
        public List<Exam> AdministeredExams { get; set; } = new List<Exam>(); 
    }
}