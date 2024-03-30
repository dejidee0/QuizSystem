namespace QuizAppSystem.Models
{
    public class AnswerOption
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
