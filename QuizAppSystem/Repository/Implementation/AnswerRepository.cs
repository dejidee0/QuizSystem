using System;
using System.Collections.Generic;
using System.Linq;
using QuizAppSystem.Models;
using QuizAppSystem.Repository;

namespace QuizAppSystem.Repositories.Implementation
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly QuizAppDbContext _context;

        public AnswerRepository(QuizAppDbContext context)
        {
            _context = context;
        }

        public Answer GetAnswer(Guid answerId)
        {
            return _context.Answers.FirstOrDefault(a => a.Id == answerId);
        }

        public IEnumerable<Answer> GetAnswers()
        {
            return _context.Answers.ToList();
        }

        public Guid CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
            return answer.Id;
        }

        public bool UpdateAnswer(Guid answerId, Answer updatedAnswer)
        {
            var existingAnswer = _context.Answers.FirstOrDefault(a => a.Id == answerId);
            if (existingAnswer == null)
                return false;

            existingAnswer.Content = updatedAnswer.Content;
            // Update other properties as needed

            _context.SaveChanges();
            return true;
        }

        public bool DeleteAnswer(Guid answerId)
        {
            var answer = _context.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return false;

            _context.Answers.Remove(answer);
            _context.SaveChanges();
            return true;
        }
    }
}
