using AutoMapper;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Repository;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Service
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public Guid CreateExam(ExamCreationDTO examDTO)
        {
            const int codeLength = 8;
            string examCode = ExamCodeGenerator.GenerateExamCode(codeLength);

            var exam = _mapper.Map<Exam>(examDTO);
            exam.ExamCode = examCode;

            return _examRepository.CreateExam(exam);
        }

        public bool DeleteExam(Guid examId)
        {
            return _examRepository.DeleteExam(examId);
        }

        public ExamDTO GetExam(Guid examId)
        {
            var exam = _examRepository.GetExam(examId);
            return _mapper.Map<ExamDTO>(exam);
        }

        public IEnumerable<ExamDTO> GetExams()
        {
            var exams = _examRepository.GetExams();
            return _mapper.Map<IEnumerable<ExamDTO>>(exams);
        }

        public bool UpdateExam(Guid examId, ExamDTO updatedExamDTO)
        {
            var exam = _mapper.Map<Exam>(updatedExamDTO);
            return _examRepository.UpdateExam(examId, exam);
        }

        public string GenerateExamCode()
        {
            const int codeLength = 8;
            return ExamCodeGenerator.GenerateExamCode(codeLength);
        }
    }
}
