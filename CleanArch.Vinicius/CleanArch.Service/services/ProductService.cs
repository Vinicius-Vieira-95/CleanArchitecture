using AutoMapper;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using CleanArch.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Service.services
{
    public class ProductService : BaseValidate<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create<TInputModel>(TInputModel model) 
            where TInputModel : class
        {
            var product = _mapper.Map<Product>(model);
            Validate(product, Activator.CreateInstance<ProductValidator>());
            await _repository.Create(product);
        }

        public async Task<ProductListDto> GetAllAsync()
        {   
            var products = await _repository.GetAllAsync();

            // List<ProductDto> list = new List<ProductDto>();
            // foreach (var product in products)
            //     list.Add(new ProductDto(product));

            return products;
        }

        public async Task<ProductDto> GetByIdAsync(int? id)
        {
            var product = await _repository.GetByIdAsync(id);
            return new ProductDto(product);
        }

        public async Task<ProductDto> Remove(int? id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.Remove(product);
            return new ProductDto(product);
        }

        public async Task<ProductDto> Update(ProductDto product)
        {
            var newProduct = _mapper.Map<Product>(product);
            Validate(newProduct, Activator.CreateInstance<ProductValidator>());
            var update = await _repository.Update(newProduct);
            return new ProductDto(newProduct);
        }
    }
}
