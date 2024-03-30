using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Repository;
using QuizAppSystem.Service.Interface;

namespace QuizAppSystem.Service.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly QuizAppDbContext _dbContext; 

        public QuestionService(IQuestionRepository questionRepository, QuizAppDbContext dbContext)
        {
            _questionRepository = questionRepository;
            _dbContext = dbContext; 
        }
        public IEnumerable<QuestionDTO> GetQuestions()
        {
            var questions = _dbContext.Questions.ToList();
            return questions.Select(MapToQuestionDTO);
        }




        public QuestionDTO GetQuestionById(Guid questionId)
        {
           
            var question = _questionRepository.GetQuestionById(questionId);
            if (question != null)
            {
                
                _dbContext.Entry(question).Collection(q => q.Options).Load();
            }
            return MapToQuestionDTO(question); 
        }

        public void CreateQuestion(QuestionCreationModel questionModel)
        {
            var question = new Question();
            question.UpdateFromCreation(questionModel);
            question.QuestionId = Guid.NewGuid();

            _questionRepository.CreateQuestion(question);
        }

        public void UpdateQuestion(Guid questionId, Question updatedQuestion)
        {
            _questionRepository.UpdateQuestion(updatedQuestion);
        }

        public void DeleteQuestion(Guid questionId)
        {
            _questionRepository.DeleteQuestion(questionId);
        }

        
        private static QuestionDTO MapToQuestionDTO(Question question)
        {
            if (question == null)
                return null;

            return new QuestionDTO
            {
                Text = question.Text,
                Type = question.Type,
                DifficultyLevel = question.DifficultyLevel,
                Subject = question.Subject,
                ExamId = question.ExamId,
                Options = question.Options?.ToList(), 
                CorrectAnswer = question.CorrectAnswer
            };
        }
    }
}
