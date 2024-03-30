using QuizAppSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppSystem.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public string DifficultyLevel { get; set; }

        public string Subject { get; set; }

        public Guid ExamId { get; set; }

        [NotMapped]
        public List<string> Options { get; set; }

        public string CorrectAnswer { get; set; }

       // public string QnInWords { get; set; }

        public string ImageName { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        // Navigation property for answers
        public List<Answer> Answers { get; set; }
        public void UpdateFromCreation(QuestionCreationModel creationModel)
        {
            Text = creationModel.Text;
            Type = creationModel.Type;
            DifficultyLevel = creationModel.DifficultyLevel;
            Subject = creationModel.Subject;
            ExamId = creationModel.ExamId;
            Options = creationModel.Options;
            CorrectAnswer = creationModel.CorrectAnswer;

        }
    }
}
