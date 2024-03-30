namespace QuizAppSystem.Models
{
    public class ExamCreationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid SubjectId { get; set; }  // Change the type to Guid
        public string ExamCode { get; set; } // Add ExamCode property
        public string DifficultyLevel { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Fees { get; set; }
        public DateTime DateOfExam { get; set; }
    }
}
