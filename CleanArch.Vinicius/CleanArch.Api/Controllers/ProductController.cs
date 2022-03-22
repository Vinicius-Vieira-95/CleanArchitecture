using CleanArch.Api.Models;
using CleanArch.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync()); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (model == null)
                return BadRequest(ModelState);
            await _service.Create(model);
            return Ok();
        }

    }
}
