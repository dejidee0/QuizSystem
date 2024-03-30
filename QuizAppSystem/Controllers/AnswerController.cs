using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.Common;
using QuizAppSystem.DTOs;
using QuizAppSystem.Service.Interface;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("{answerId}")]
        public ApiResponse<AnswerDTO> GetAnswer(Guid answerId)
        {
            var response = new ApiResponse<AnswerDTO>();

            try
            {
                var answer = _answerService.GetAnswer(answerId);
                if (answer == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Answer not found";
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.Data = answer;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public ApiResponse<Guid> CreateAnswer(AnswerDTO answerDTO)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                var answerId = _answerService.CreateAnswer(answerDTO);
                response.StatusCode = 201;
                response.Message = "Answer created successfully";
                response.Data = answerId;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpPut("{answerId}")]
        public ApiResponse<bool> UpdateAnswer(Guid answerId, AnswerDTO updatedAnswerDTO)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var success = _answerService.UpdateAnswer(answerId, updatedAnswerDTO);
                if (success)
                {
                    response.StatusCode = 200;
                    response.Message = "Answer updated successfully";
                    response.Data = true;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Answer not found";
                    response.Data = false;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        [HttpDelete("{answerId}")]
        public ApiResponse<bool> DeleteAnswer(Guid answerId)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var success = _answerService.DeleteAnswer(answerId);
                if (success)
                {
                    response.StatusCode = 200;
                    response.Message = "Answer deleted successfully";
                    response.Data = true;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Answer not found";
                    response.Data = false;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }
    }
}
