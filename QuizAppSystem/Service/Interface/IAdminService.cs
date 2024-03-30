using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Service
{
    public interface IAdminService
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Exam> GetAllExams();
        bool EnableExam(Guid examId);
        bool DisableExam(Guid examId);
        //string GeneratePIN(Guid examId);
        IEnumerable<ExamReportDto> GetExamReports(Guid examId); // Change the return type
    }
}
