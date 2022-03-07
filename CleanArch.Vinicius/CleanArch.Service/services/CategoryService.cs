﻿using CleanArch.Domain.Entities;
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
            var cat = _mapper.Map<Category>(model);
            Validate(cat, Activator.CreateInstance<CategoryValidator>());
            await _repository.Create(cat);
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
            return new CategoryDto(category);
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
