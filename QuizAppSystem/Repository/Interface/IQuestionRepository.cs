using QuizAppSystem.Models;
using System;
using System.Linq;

namespace QuizAppSystem.Repository
{
    public interface IQuestionRepository
    {
        IQueryable<Question> GetQuestionsQueryable(); // Add this method
        Question GetQuestionById(Guid questionId);
        void CreateQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(Guid questionId);
    }
}
