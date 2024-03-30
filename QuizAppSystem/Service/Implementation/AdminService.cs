using Microsoft.EntityFrameworkCore;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizAppSystem.Service.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly QuizAppDbContext _context;

        public AdminService(QuizAppDbContext context)
        {
            _context = context;
        }

        public string GenerateExamCode(Guid examId)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == examId);

            if (exam != null)
            {
                var examCode = GenerateUniqueExamCode(exam);
                exam.SubjectId = new Guid(examCode); // Convert the string code to Guid
                _context.SaveChanges();
                return $"Exam code generated successfully for Exam: {exam.Title}. Exam Code: {examCode}";
            }

            return "Exam not found.";
        }





        private string GenerateRandomExamCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8); 
        }
        private string GenerateUniqueExamCode(Exam exam)
        {
            string examCode;
            do
            {
                examCode = GenerateRandomExamCode();
            } while (_context.Exams.Any(e => e.SubjectId == new Guid(examCode)));


            return examCode;
        }


        public IEnumerable<User> GetAllUsers()
        {
            
            var identityUsers = _context.Users.ToList();

           
            return identityUsers.Select(identityUser => (User)identityUser).ToList();
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exams.ToList();
        }

        public bool EnableExam(Guid examId)
        {
            return ChangeExamStatus(examId, true);
        }

        public bool DisableExam(Guid examId)
        {
            return ChangeExamStatus(examId, false);
        }

        public IEnumerable<ExamReportDto> GetExamReports(Guid examId)
        {
            var exam = _context.Exams
                .Include(e => e.Participants)
                .FirstOrDefault(e => e.Id == examId);

            if (exam != null)
            {
                var reports = exam.Participants.Select(p => new ExamReportDto
                {
                    Id = Guid.NewGuid(), // Example ID generation
                    ExamId = exam.Id,
                    ParticipantId = p.Id.ToString(),
                    ParticipantFirstName = p.FirstName,
                    ParticipantLastName = p.LastName,
                    ParticipantEmail = p.Email,
                    StartTime = p.StartedAt,
                    EndTime = p.CompletedAt,
                    Score = 0, // Example score
                    IsSuccessful = p.IsSuccessful
                });

                return reports;
            }

            return Enumerable.Empty<ExamReportDto>(); // Return an empty enumerable if the exam is not found
        }

        private bool ChangeExamStatus(Guid examId, bool isActive)
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
    }
}
