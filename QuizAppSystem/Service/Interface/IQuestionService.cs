using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Service.Interface
{
    public interface IQuestionService
    {
        IEnumerable<QuestionDTO> GetQuestions();

        QuestionDTO GetQuestionById(Guid questionId);
        void CreateQuestion(QuestionCreationModel questionModel);
        void UpdateQuestion(Guid questionId, Question updatedQuestion);
        void DeleteQuestion(Guid questionId);
       
    }
}
