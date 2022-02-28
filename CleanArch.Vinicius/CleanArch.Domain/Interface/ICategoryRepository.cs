using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interface
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(int? id);
        public Task<Category> Create(Category category);
        public Task<Category> Update(Category category);
        public Task<Category> Remove(Category category);
    }
}