using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiSolution
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet("Get All Products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
           
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("Get Products By Id {id}")]
        public ActionResult<Product> GetProductById(int id)
        {
           

            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid product ID." });
            }

            try
            {
                var product = _productService.GetProductById(id);
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/products
        [HttpPost("Add products")]
        public ActionResult AddProduct([FromBody] Product product)
        {
            

            if (product == null)
                return BadRequest(new { Message = "Product cannot be null." });

            if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
                return BadRequest(new { Message = "Invalid product data. Name cannot be empty and price must be greater than 0." });

            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        
        [HttpPut(" update Product{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            Console.WriteLine($"Action: Update product with ID - {id}");

            if (id <= 0 || updatedProduct == null || updatedProduct.Id != id)
                return BadRequest(new { Message = "Invalid product ID or data." });

            try
            {
                _productService.UpdateProduct(updatedProduct);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete(" Detate Product By Id{id}")]
        public ActionResult DeleteProduct(int id)
        {
           

            if (id <= 0)
                return BadRequest(new { Message = "Invalid product ID." });

            try
            {
                _productService.DeleteProductById(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        
        [HttpGet(" Get Product By category/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
           

            if (categoryId <= 0)
            {
                return BadRequest(new { Message = "Invalid category ID." });
            }

            var products = _productService.GetAllProducts()
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            if (!products.Any())
            {
                return NotFound(new { Message = $"No products found for category {categoryId}." });
            }

            return Ok(products);
        }

       
        [HttpGet(" Get Total price By category/{categoryId}")]
        public ActionResult<decimal> GetTotalPriceByCategory(int categoryId)
        {
            Console.WriteLine($"Action: Get total price of products in category - {categoryId}");

            if (categoryId <= 0)
            {
                return BadRequest(new { Message = "Invalid category ID." });
            }

            var products = _productService.GetAllProducts()
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            if (!products.Any())
            {
                return NotFound(new { Message = $"No products found for category {categoryId}." });
            }

            var totalPrice = products.Sum(p => p.Price);
            return Ok(totalPrice);
        }

        
        [HttpGet("totalprice-per-category")]
        public ActionResult<Dictionary<int, decimal>> GetTotalPricePerCategory()
        {
            Console.WriteLine("Action: Get total price of products per category");

            var totalPricePerCategory = _productService.GetAllProducts()
                .GroupBy(p => p.CategoryId)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Price));

            return Ok(totalPricePerCategory);
        }
    }

}

