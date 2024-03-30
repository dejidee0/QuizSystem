using QuizAppSystem.Models;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Repository
{
    public interface IExamRepository
    {
        IEnumerable<Exam> GetExams();
        Exam GetExam(Guid examId);
        Guid CreateExam(Exam exam);
        bool UpdateExam(Guid examId, Exam updatedExam);
        bool DeleteExam(Guid examId);
    }
}
