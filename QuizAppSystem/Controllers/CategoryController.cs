using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.Common;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppSystem.Controllers
{
    //[Authorize(Roles = "Examiner")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(new ApiResponse<List<CategoryDto>>
            {
                Data = categoryDtos,
                StatusCode = 200,
                Message = "Success"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound(new ApiResponse<CategoryDto>
                {
                    StatusCode = 404,
                    Message = "Category not found.",
                    Error = "Category not found."
                });
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(new ApiResponse<CategoryDto>
            {
                Data = categoryDto,
                StatusCode = 200,
                Message = "Success"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string categoryName)
        {
            try
            {
                var categoryId = await _categoryService.CreateCategory(categoryName);
                return CreatedAtAction(nameof(GetCategory), new { id = categoryId }, null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Bad request",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var success = await _categoryService.UpdateCategory(id, category);

            if (success)
            {
                return NoContent();
            }

            return NotFound(new ApiResponse<CategoryDto>
            {
                StatusCode = 404,
                Message = "Category not found.",
                Error = "Category not found."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var success = await _categoryService.DeleteCategory(id);

            if (success)
            {
                return NoContent();
            }

            return NotFound(new ApiResponse<CategoryDto>
            {
                StatusCode = 404,
                Message = "Category not found.",
                Error = "Category not found."
            });
        }
    }
}
