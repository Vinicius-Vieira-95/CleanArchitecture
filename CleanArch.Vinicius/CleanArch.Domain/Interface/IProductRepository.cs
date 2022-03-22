using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interface
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int? id);
        public Task Create(Product product);
        public Task<Product> Update(Product product);
        public Task<Product> Remove(Product product);
    }
}