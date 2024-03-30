using QuizAppSystem.Models.Enums;

namespace QuizAppSystem.Models
{
    public class TransactionCreationModel
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
    }
}
