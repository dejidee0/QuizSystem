﻿using System;

namespace QuizAppSystem.DTOs
{
    public class AnswerDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; } // Foreign key

    }
}
