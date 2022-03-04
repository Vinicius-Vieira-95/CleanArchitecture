using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Domain.DTOs;
using FluentValidation;

namespace CleanArch.Service.services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Create<TInputModel, TValidator>(TInputModel model)
            where TInputModel : class
            where TValidator : AbstractValidator<Category>
        {
            var category = _mapper.Map<Category>(model);
            await _repository.Create(category);
            return new CategoryDto (await _repository.GetByIdAsync(category.Id));
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categoriesList = await _repository.GetAllAsync();
            return categoriesList;
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
           var category = await _repository.GetByIdAsync(id);
           return category;
        }

        public async Task<Category> Remove(int? category)
        {
            var cat = await _repository.GetByIdAsync(category.Value);
            await _repository.Remove(cat);
            return cat;
        }

        public async Task<Category> Update(int? category)
        {
           var cat = await _repository.GetByIdAsync(category);
           return await _repository.Update(cat);
        }
    }
}
