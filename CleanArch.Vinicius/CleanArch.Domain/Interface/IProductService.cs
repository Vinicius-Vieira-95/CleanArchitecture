using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interface
{
    public interface IProductService
    {
        public Task<ProductListDto> GetAllAsync();
        public Task<ProductDto> GetByIdAsync(int? id);
        public Task Create<TInputModel>(TInputModel model)
            where TInputModel : class;
        public Task<ProductDto> Update(ProductDto product);
        public Task<ProductDto> Remove(int? id);
    }
}