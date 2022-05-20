using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using CleanArch.Infra.Data.Banco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductListDto> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            var query = _context.Products
                .Include(x => x.Category)
                .AsQueryable();
            
            var products = await getProducts(query).ToListAsync();

            return new ProductListDto { list = products
            .Select( x => new ProductDto(x)).ToList()
            };
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            // var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return await _context.Products
                .Include(x => x.Category)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Product> Update(Product product)
        {
            var update = await _context.Products.FindAsync(product.Id);
            update.Name = product.Name;
            update.Description = product.Description;
            update.Stock = product.Stock;
            update.Price = product.Price;
            update.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();
            return update;
        }

        private IQueryable<Product> getProducts(IQueryable<Product> query)
        {
            return query.OrderBy(x => x.Id);
        }
    }
}
