using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interface
{
    public interface IProductService
    {
         Task<IEnumerable<Product>> GetAllAsync();
         Task<Product> GetByIdAsync(int? id);
         Task<Product> Create(Product product);
         Task<Product> Update(Product product);
         Task<Product> Remove(Product product);
    }
}