using System;
using System.Collections.Generic;
using QuizAppSystem.Models.Enums;

namespace QuizAppSystem.Models
{

    public class Transaction
    {

        public int Id { get; set; }
        public int WalletId { get; set; } // Foreign key to Wallet
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public TransactionType Type { get; set; }
        public ICollection<Transaction> RelatedTransactions { get; set; }
    }
}
