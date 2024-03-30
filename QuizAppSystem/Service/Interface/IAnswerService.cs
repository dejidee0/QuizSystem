using System;
using System.Collections.Generic;
using QuizAppSystem.DTOs;

namespace QuizAppSystem.Service.Interface
{
    public interface IAnswerService
    {
        AnswerDTO GetAnswer(Guid answerId);
        IEnumerable<AnswerDTO> GetAnswers();
        Guid CreateAnswer(AnswerDTO answerDTO);
        bool UpdateAnswer(Guid answerId, AnswerDTO updatedAnswerDTO);
        bool DeleteAnswer(Guid answerId);
    }
}
