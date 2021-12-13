using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interface
{
    public interface ICategoryService
    {
         Task<IEnumerable<Category>> GetAllAsync();
         Task<Category> GetByIdAsync(int? id);
         Task<Category> Create(Category category);
         Task<Category> Update(Category category);
         Task<Category> Remove(Category category);
    }
}