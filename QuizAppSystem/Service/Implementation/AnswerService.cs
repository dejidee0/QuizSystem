using AutoMapper;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Repository;
using QuizAppSystem.Service.Interface;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Service.Implementation
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public Guid CreateAnswer(AnswerDTO answerDTO)
        {
            var answer = _mapper.Map<Answer>(answerDTO);
            return _answerRepository.CreateAnswer(answer);
        }

        public bool DeleteAnswer(Guid answerId)
        {
            return _answerRepository.DeleteAnswer(answerId);
        }

        public AnswerDTO GetAnswer(Guid answerId)
        {
            var answer = _answerRepository.GetAnswer(answerId);
            return _mapper.Map<AnswerDTO>(answer);
        }

        public IEnumerable<AnswerDTO> GetAnswers()
        {
            var answers = _answerRepository.GetAnswers();
            return _mapper.Map<IEnumerable<AnswerDTO>>(answers);
        }

        public bool UpdateAnswer(Guid answerId, AnswerDTO updatedAnswerDTO)
        {
            var updatedAnswer = _mapper.Map<Answer>(updatedAnswerDTO);
            return _answerRepository.UpdateAnswer(answerId, updatedAnswer);
        }
    }
}
