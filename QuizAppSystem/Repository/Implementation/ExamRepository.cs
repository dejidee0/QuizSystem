using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizAppSystem.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly QuizAppDbContext _dbContext;

        public ExamRepository(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid CreateExam(Exam exam)
        {
            _dbContext.Exams.Add(exam);
            _dbContext.SaveChanges();
            return exam.Id;
        }

        public bool DeleteExam(Guid examId)
        {
            var exam = _dbContext.Exams.Find(examId);
            if (exam == null)
                return false;

            _dbContext.Exams.Remove(exam);
            _dbContext.SaveChanges();
            return true;
        }

        public Exam GetExam(Guid examId)
        {
            return _dbContext.Exams.Find(examId);
        }

        public IEnumerable<Exam> GetExams()
        {
            return _dbContext.Exams.ToList();
        }

        public bool UpdateExam(Guid examId, Exam updatedExam)
        {
            var exam = _dbContext.Exams.Find(examId);
            if (exam == null)
                return false;

            // Update exam properties
            exam.Title = updatedExam.Title;
            exam.Description = updatedExam.Description;
            exam.DifficultyLevel = updatedExam.DifficultyLevel;
            exam.StartTime = updatedExam.StartTime;
            exam.EndTime = updatedExam.EndTime;
            exam.Duration = updatedExam.Duration;
            exam.Fees = updatedExam.Fees;
            exam.DateOfExam = updatedExam.DateOfExam;
            exam.IsActive = updatedExam.IsActive;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
