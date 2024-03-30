using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.Common;
using QuizAppSystem.DTOs;
using QuizAppSystem.Service;
using System;
using System.Collections.Generic;

namespace QuizAppSystem.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet("Users")]
        public ActionResult<ApiResponse<List<UserDto>>> GetAllUsers()
        {
            var users = _adminService.GetAllUsers();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return Ok(new ApiResponse<List<UserDto>>
            {
                Data = userDtos,
                StatusCode = 200,
                Message = "Success"
            });
        }

        [HttpGet("Exams")]
        public ActionResult<ApiResponse<List<ExamDTO>>> GetAllExams()
        {
            var exams = _adminService.GetAllExams();
            var examDtos = _mapper.Map<List<ExamDTO>>(exams);

            return Ok(new ApiResponse<List<ExamDTO>>
            {
                Data = examDtos,
                StatusCode = 200,
                Message = "Success"
            });
        }

        [HttpPut("EnableExam/{examId}")]
        public ActionResult<ApiResponse<string>> EnableExam(Guid examId)
        {
            var success = _adminService.EnableExam(examId);

            if (success)
                return Ok(new ApiResponse<string>
                {
                    Data = "Exam enabled successfully.",
                    StatusCode = 200,
                    Message = "Success"
                });

            return NotFound(new ApiResponse<string>
            {
                StatusCode = 404,
                Message = "Exam not found.",
                Error = "Exam not found."
            });
        }

        [HttpPut("DisableExam/{examId}")]
        public ActionResult<ApiResponse<string>> DisableExam(Guid examId)
        {
            var success = _adminService.DisableExam(examId);

            if (success)
                return Ok(new ApiResponse<string>
                {
                    Data = "Exam disabled successfully.",
                    StatusCode = 200,
                    Message = "Success"
                });

            return NotFound(new ApiResponse<string>
            {
                StatusCode = 404,
                Message = "Exam not found.",
                Error = "Exam not found."
            });
        }

        //[HttpPost("PINGeneration/{examId}")]
        //public ActionResult<ApiResponse<string>> GeneratePIN(Guid examId)
        //{
        //    var result = _adminService.GeneratePIN(examId);
        //    return Ok(new ApiResponse<string>
        //    {
        //        Data = result,
        //        StatusCode = 200,
        //        Message = "Success"
        //    });
        //}

        [HttpGet("Reports/{examId}")]
        public ActionResult<ApiResponse<List<ExamReportDto>>> GetExamReports(Guid examId)
        {
            var reports = _adminService.GetExamReports(examId).ToList(); // Cast to List<ExamReportDto>
            if (reports != null)
            {
                return Ok(new ApiResponse<List<ExamReportDto>>
                {
                    Data = reports,
                    StatusCode = 200,
                    Message = "Success"
                });
            }

            return NotFound(new ApiResponse<List<ExamReportDto>>
            {
                StatusCode = 404,
                Message = "Exam not found.",
                Error = "Exam not found."
            });
        }
    }
}
