using QuizAppSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppSystem.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Task<Category> GetById(Guid id);
        Task Add(Category category);
        void Update(Category category);
        Task Remove(Guid id);
    }
}
