using QuizAppSystem.Models.Enums;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Models
{
    public class QuestionCreationModel
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string DifficultyLevel { get; set; }
        public string Subject { get; set; }
        public Guid ExamId { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
       // public string QnInWords { get; set; }
        public string ImageName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

    }
}
