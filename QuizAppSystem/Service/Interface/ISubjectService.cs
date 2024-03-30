using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppSystem.Models;

namespace QuizAppSystem.Service.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjects();
        Task<Subject> GetSubjectById(Guid id);
        Task<Guid> CreateSubject(string subjectName);
        Task<bool> UpdateSubject(Guid id, Subject subject);
        Task<bool> DeleteSubject(Guid id);
    }
}
