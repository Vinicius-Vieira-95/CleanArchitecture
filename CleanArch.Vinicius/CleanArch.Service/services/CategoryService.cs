using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Domain.DTOs;
using FluentValidation;
using System;
using CleanArch.Domain.Validation;

namespace CleanArch.Service.services
{
    public class CategoryService : BaseValidate<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create<TInputModel>(TInputModel model)
            where TInputModel : class
        {
            var category = _mapper.Map<Category>(model);
            Validate(category, Activator.CreateInstance<CategoryValidator>());
            await _repository.Create(category);
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categoriesList = await _repository.GetAllAsync();
            //List<CategoryDto> result = new List<CategoryDto>();
            //foreach (var category in categoriesList)
            //{
            //    result.Add (new CategoryDto (category));
            //}

            var list = categoriesList.ConvertAll(new Converter<Category, CategoryDto>(CategoryToCategoryDto));
            return list;
        }

        private CategoryDto CategoryToCategoryDto(Category cat)
        {
            return new CategoryDto(cat);
        }

        public async Task<CategoryDto> GetByIdAsync(int? id)
        {
            var category = await _repository.GetByIdAsync(id);
            if(category == null)
                   return null;
            return new CategoryDto(category);
        }

        public async Task<CategoryDto> Remove(int? id)
        {
            var cat = await _repository.GetByIdAsync(id);
            await _repository.Remove(cat);
            return new CategoryDto(cat);
        }

        public async Task<CategoryDto> Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            Validate(category, Activator.CreateInstance<CategoryValidator>());
            await _repository.Update(category);

            return new CategoryDto(category);
        }
    }
}
