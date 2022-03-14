using AutoMapper;
using CleanArch.Api.Models;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interface;
using CleanArch.Domain.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryModel cat)
        {
            await _service.Create(cat);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet, Route("{id}")]
        public  async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _service.GetByIdAsync(id);
            if(category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _service.Remove(category.Id);
            return NoContent();
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryModel categoryModel)
        {
            var cat = await _service.GetByIdAsync(id);
            if(cat == null)
                return BadRequest();

            cat.Name = categoryModel.Name;
            await _service.Update(cat);
            return Ok();
        }

    }
}
