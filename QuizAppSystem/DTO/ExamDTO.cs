using QuizAppSystem.Models.Enums;
using System;

namespace QuizAppSystem.DTOs
{
    public class ExamDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public int Duration { get; set; }
        public string ExaminerId { get; set; }
        public Guid SubjectId { get; set; }  
        public string ExamCode { get; set; } 
        public decimal Fees { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime DateOfExam { get; set; }
        public bool IsActive { get; set; }
    }
}
