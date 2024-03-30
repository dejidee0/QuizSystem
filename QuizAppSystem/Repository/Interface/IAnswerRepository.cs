using System;
using System.Collections.Generic;
using QuizAppSystem.Models;

namespace QuizAppSystem.Repository
{
    public interface IAnswerRepository
    {
        Answer GetAnswer(Guid answerId);
        IEnumerable<Answer> GetAnswers();
        Guid CreateAnswer(Answer answer);
        bool UpdateAnswer(Guid answerId, Answer updatedAnswer);
        bool DeleteAnswer(Guid answerId);
    }
}
