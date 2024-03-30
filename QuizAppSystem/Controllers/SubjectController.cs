using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using QuizAppSystem.Service.Interface;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using QuizAppSystem.Common;

namespace QuizAppSystem.Controllers
{
    //[Authorize(Roles = "Examiner")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }


        // GET: api/Subject
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<Subject>>> GetAllSubjects()
        {
            var response = new ApiResponse<IEnumerable<Subject>>();

            try
            {
                var subjects = await _subjectService.GetAllSubjects();
                response.StatusCode = 200;
                response.Message = "Success";
                response.Data = subjects;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<Subject>> GetSubject(Guid id)
        {
            var response = new ApiResponse<Subject>();

            try
            {
                var subject = await _subjectService.GetSubjectById(id);
                if (subject == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Subject not found";
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.Data = subject;
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

        // POST: api/Subject
        [HttpPost]
        public async Task<ApiResponse<Guid>> CreateSubject([FromBody] string subjectName)
        {
            var response = new ApiResponse<Guid>();

            try
            {
                if (string.IsNullOrEmpty(subjectName))
                {
                    response.StatusCode = 400;
                    response.Message = "Subject name cannot be empty.";
                    return response;
                }

                var subjectId = await _subjectService.CreateSubject(subjectName);
                response.StatusCode = 201;
                response.Message = "Subject created successfully";
                response.Data = subjectId;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }

            return response;
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<ApiResponse<bool>> UpdateSubject(Guid id, [FromBody] Subject subject)
        {
            var response = new ApiResponse<bool>();

            try
            {
                if (id != subject.Id)
                {
                    response.StatusCode = 400;
                    response.Message = "Bad request";
                    return response;
                }

                var updated = await _subjectService.UpdateSubject(id, subject);

                if (!updated)
                {
                    response.StatusCode = 404;
                    response.Message = "Subject not found or invalid subject data.";
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Subject updated successfully";
                    response.Data = true;
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

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteSubject(Guid id)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var deleted = await _subjectService.DeleteSubject(id);

                if (!deleted)
                {
                    response.StatusCode = 404;
                    response.Message = "Subject not found";
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Subject deleted successfully";
                    response.Data = true;
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
