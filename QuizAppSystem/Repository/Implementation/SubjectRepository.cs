using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;

namespace QuizAppSystem.Repository.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuizAppDbContext _dbContext;

        public SubjectRepository(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            return await _dbContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectById(Guid id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }

        public async Task<Guid> CreateSubject(string subjectName)
        {
            var subject = new Subject
            {
                Name = subjectName
            };

            _dbContext.Subjects.Add(subject);
            await _dbContext.SaveChangesAsync();

            return subject.Id;
        }

        public async Task<bool> UpdateSubject(Guid id, Subject subject)
        {
            if (id != subject.Id)
            {
                return false;
            }

            _dbContext.Entry(subject).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
                {
                    return false;
                }

                throw;
            }

            return true;
        }

        public async Task<bool> DeleteSubject(Guid id)
        {
            var subject = await _dbContext.Subjects.FindAsync(id);

            if (subject == null)
            {
                return false;
            }

            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool SubjectExists(Guid id)
        {
            return _dbContext.Subjects.Any(e => e.Id == id);
        }
    }
}
