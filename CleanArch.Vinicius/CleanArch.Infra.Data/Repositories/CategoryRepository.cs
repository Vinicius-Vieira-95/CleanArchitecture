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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> Remove(int? id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
            return result;     
        }

        public async Task<Category> Remove(Category category)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            _context.Categories.Remove(result);
            _context.SaveChanges();
            return result;
        }

        public async Task<Category> Update(Category category)
        {
            var update = await _context.Categories.FindAsync(category.Id);
            update.Name = category.Name;
            await _context.SaveChangesAsync();
            return update;
        }

    }
}
