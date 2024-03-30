using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using QuizAppSystem.Models.Enums;

namespace QuizAppSystem.Models
{
    public class Exam
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? SubjectId { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }   
        public string ExamCode { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Fees { get; set; }
        public DateTime DateOfExam { get; set; }

        [ForeignKey("Examiner")]
        public string ExaminerId { get; set; }
        public Examiner Examiner { get; set; }

        [ForeignKey("CreatedByExaminer")]
        public string CreatedByExaminerId { get; set; }
        public Examiner CreatedByExaminer { get; set; }

        public ICollection<Question> Questions { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
