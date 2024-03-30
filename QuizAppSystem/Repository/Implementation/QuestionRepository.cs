using System;
using System.Linq;
using QuizAppSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizAppSystem.Repository.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizAppDbContext _context;

        public QuestionRepository(QuizAppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Question> GetQuestionsQueryable()
        {
            return _context.Questions.AsQueryable();
        }

        public Question GetQuestionById(Guid questionId)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == questionId);
        }

        public void CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public void UpdateQuestion(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuestion(Guid questionId)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }
    }
}
