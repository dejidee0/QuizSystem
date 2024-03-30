using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppSystem.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Task<Category> GetCategory(Guid id);
        Task<Guid> CreateCategory(string categoryName);
        Task<bool> UpdateCategory(Guid id, Category category);
        Task<bool> DeleteCategory(Guid id);
    }
}
