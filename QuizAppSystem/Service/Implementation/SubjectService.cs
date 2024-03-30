using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;
using QuizAppSystem.Service.Interface;

namespace QuizAppSystem.Service.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            return await _subjectRepository.GetAllSubjects();
        }

        public async Task<Subject> GetSubjectById(Guid id)
        {
            return await _subjectRepository.GetSubjectById(id);
        }

        public async Task<Guid> CreateSubject(string subjectName)
        {
            return await _subjectRepository.CreateSubject(subjectName);
        }

        public async Task<bool> UpdateSubject(Guid id, Subject subject)
        {
            return await _subjectRepository.UpdateSubject(id, subject);
        }

        public async Task<bool> DeleteSubject(Guid id)
        {
            return await _subjectRepository.DeleteSubject(id);
        }
    }
}
