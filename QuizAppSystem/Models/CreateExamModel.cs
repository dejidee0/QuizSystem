using QuizAppSystem.Models.Enums;

namespace QuizAppSystem.Models
{
    public class CreateExamModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public DateTime? StartTime { get; set; } 
        public DateTime? EndTime { get; set; } 
        public int Duration { get; set; }
    }
}
