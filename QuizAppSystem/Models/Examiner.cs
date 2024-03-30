namespace QuizAppSystem.Models
{
    public class Examiner : User
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Exam> CreatedExams { get; set; } = new List<Exam>();
        public List<Exam> ExaminedExams { get; set; } = new List<Exam>();

    }
}
