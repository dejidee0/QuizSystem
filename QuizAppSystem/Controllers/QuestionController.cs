using Microsoft.AspNetCore.Mvc;
using System;
using QuizAppSystem.Models;
using QuizAppSystem.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using QuizAppSystem.Common;
using QuizAppSystem.DTOs; // Import ApiResponse<T>

namespace QuizAppSystem.Controllers
{
    //[Authorize(Roles = "Examiner")]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public ApiResponse<IEnumerable<QuestionDTO>> GetQuestions()
        {
            var questions = _questionService.GetQuestions();
            return new ApiResponse<IEnumerable<QuestionDTO>>
            {
                Data = questions,
                StatusCode = 200,
                Message = "Success"
            };
        }

        [HttpGet("{questionId}")]
        public ApiResponse<QuestionDTO> GetQuestion(Guid questionId)
        {
            var question = _questionService.GetQuestionById(questionId);

            if (question != null)
            {
                return new ApiResponse<QuestionDTO>
                {
                    Data = question,
                    StatusCode = 200,
                    Message = "Success"
                };
            }

            return new ApiResponse<QuestionDTO>
            {
                StatusCode = 404,
                Message = "Question not found."
            };
        }

        [HttpPost]
        public ApiResponse<string> CreateQuestion([FromBody] QuestionCreationModel questionModel)
        {
            if (questionModel != null)
            {
                _questionService.CreateQuestion(questionModel);
                return new ApiResponse<string>
                {
                    Data = "Question created successfully.",
                    StatusCode = 200,
                    Message = "Success"
                };
            }

            return new ApiResponse<string>
            {
                StatusCode = 400,
                Message = "Invalid question data."
            };
        }

        [HttpPut("{questionId}")]
        public ApiResponse<string> UpdateQuestion(Guid questionId, [FromBody] Question updatedQuestion)
        {
            if (updatedQuestion != null)
            {
                _questionService.UpdateQuestion(questionId, updatedQuestion);
                return new ApiResponse<string>
                {
                    Data = "Question updated successfully.",
                    StatusCode = 200,
                    Message = "Success"
                };
            }

            return new ApiResponse<string>
            {
                StatusCode = 400,
                Message = "Invalid question data."
            };
        }

        [HttpDelete("{questionId}")]
        public ApiResponse<string> DeleteQuestion(Guid questionId)
        {
            _questionService.DeleteQuestion(questionId);
            return new ApiResponse<string>
            {
                Data = "Question deleted successfully.",
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
}
