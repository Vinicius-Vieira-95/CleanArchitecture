using AutoMapper;
using CleanArch.Api.Models;
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
            var result = await _service.Create<CategoryModel, CategoryValidator>(cat);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

    }
}
