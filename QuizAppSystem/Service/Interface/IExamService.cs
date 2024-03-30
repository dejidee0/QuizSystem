using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Repository;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Service
{
    public interface IExamService
    {
        IEnumerable<ExamDTO> GetExams();
        ExamDTO GetExam(Guid examId);
        Guid CreateExam(ExamCreationDTO examDTO);
        bool UpdateExam(Guid examId, ExamDTO updatedExamDTO);
        bool DeleteExam(Guid examId);
        string GenerateExamCode();
    }
}
