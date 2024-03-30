using System;
using System.Collections.Generic;
using QuizAppSystem.Models;

namespace QuizAppSystem.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Exam> GetAllExams();
        bool ChangeExamStatus(Guid examId, bool isActive);
       // string GenerateUniquePIN(Exam exam);
    }
}
