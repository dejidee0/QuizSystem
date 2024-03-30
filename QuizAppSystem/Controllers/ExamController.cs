using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.Common;
using QuizAppSystem.DTOs;
using QuizAppSystem.Service;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("generate-code")]
        public IActionResult GenerateExamCode()
        {
            string examCode = _examService.GenerateExamCode();
            return Ok(new { examCode });
        }

        [HttpGet]
        public IActionResult GetExams()
        {
            var exams = _examService.GetExams();
            var response = new ApiResponse<IEnumerable<ExamDTO>>
            {
                Data = exams,
                StatusCode = 200,
                Message = "Success"
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetExam(Guid id)
        {
            var exam = _examService.GetExam(id);
            if (exam == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = "Exam not found"
                });
            }

            var response = new ApiResponse<ExamDTO>
            {
                Data = exam,
                StatusCode = 200,
                Message = "Success"
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateExam([FromBody] ExamCreationDTO examDTO)
        {
            var examId = _examService.CreateExam(examDTO);
            var response = new ApiResponse<Guid>
            {
                Data = examId,
                StatusCode = 201,
                Message = "Exam created successfully"
            };
            return CreatedAtAction(nameof(GetExam), new { id = examId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExam(Guid id, [FromBody] ExamDTO examDTO)
        {
            var success = _examService.UpdateExam(id, examDTO);
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = "Exam not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Exam updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExam(Guid id)
        {
            var success = _examService.DeleteExam(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = 404,
                    Message = "Exam not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Exam deleted successfully"
            });
        }
    }
}
