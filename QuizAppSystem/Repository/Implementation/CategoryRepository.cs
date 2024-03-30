using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAppSystem.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizAppDbContext _dbContext;

        public CategoryRepository(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task Add(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task Remove(Guid id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
