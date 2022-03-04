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
         public Task<Category> GetByIdAsync(int? id);
         public Task<CategoryDto> Create<TInputModel, TValidator>(TInputModel model)
            where TInputModel: class
            where TValidator : AbstractValidator<Category>;
         public Task<Category> Update(int? category);
         public Task<Category> Remove(int? category);
    }
}