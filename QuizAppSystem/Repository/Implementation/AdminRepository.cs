using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;

namespace QuizAppSystem.Repositories.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly QuizAppDbContext _context;

        public AdminRepository(QuizAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.User> GetAllUsers()
        {
            return _context.Users.ToList() as IEnumerable<Models.User>;
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exams.ToList();
        }

        public bool ChangeExamStatus(Guid examId, bool isActive)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == examId);

            if (exam != null)
            {
                exam.IsActive = isActive;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        //public string GenerateUniquePIN(Exam exam)
        //{
        //    string pin;
        //    do
        //    {
        //        pin = GenerateRandomPIN();
        //    } while (exam.Participants.Any(p => p.PIN == pin) || exam.PIN == pin);

        //    return pin;
        //}

        private string GenerateRandomPIN()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
