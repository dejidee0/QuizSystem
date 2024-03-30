using System.Transactions;

namespace QuizAppSystem.Models
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentGateway { get; set; }
        public string TransactionId { get; set; }
        public TransactionStatus Status { get; set; }

        public string ExaminerId { get; set; }
        public Examiner Examiner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
