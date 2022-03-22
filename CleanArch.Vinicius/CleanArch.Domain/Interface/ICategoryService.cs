using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using FluentValidation;

namespace CleanArch.Domain.Interface
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> GetAllAsync();
        public Task<CategoryDto> GetByIdAsync(int? id);
        public Task Create<TInputModel>(TInputModel model)
            where TInputModel : class;
        public Task<CategoryDto> Update(CategoryDto category);
        public Task<CategoryDto> Remove(int? id);
    }
}