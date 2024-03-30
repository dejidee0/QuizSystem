using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAppSystem.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly QuizAppDbContext _dbContext;

        public CategoryService(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Guid> CreateCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("Category name cannot be empty.", nameof(categoryName));
            }

            var category = new Category
            {
                Name = categoryName
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> UpdateCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return false;
            }

            _dbContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool CategoryExists(Guid id)
        {
            return _dbContext.Categories.Any(e => e.Id == id);
        }
    }
}
