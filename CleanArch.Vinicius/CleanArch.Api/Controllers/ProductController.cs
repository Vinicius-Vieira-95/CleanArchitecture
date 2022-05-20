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
    
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _service.GetByIdAsync(id);
            if(product == null)
                return NotFound("Product not found");
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (model == null)
                return BadRequest(ModelState);
            await _service.Create(model);
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _service.GetByIdAsync(id);
            if(user == null)
                return NotFound("User not found");

            await _service.Remove(id);
            return NoContent();
        }
        
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id ,[FromBody] ProductModel productmodel)
        {
            var product = await _service.GetByIdAsync(id);
            if(product == null)
                return BadRequest();
            
            product.Name = productmodel.Name;
            product.Price = productmodel.Price;
            product.Stock = productmodel.Stock;
            product.Description = productmodel.Description;

            await _service.Update(product);

            return NoContent();
        }


    }
}
