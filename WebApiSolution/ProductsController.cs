using domain;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;

namespace WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllProductsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _service.AddProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            await _service.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId) =>
            Ok(await _service.GetProductsByCategoryAsync(categoryId));

        [HttpGet("category/{categoryId}/total-price")]
        public async Task<IActionResult> GetTotalPriceByCategory(int categoryId) =>
            Ok(await _service.GetTotalPriceByCategoryAsync());

        [HttpGet("categories/total-price")]
        public async Task<IActionResult> GetTotalPricePerCategory() =>
            Ok(await _service.GetTotalPricePerCategoryAsync());
    }
}

