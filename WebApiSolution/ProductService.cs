using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _context.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) =>
            await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId) =>
            await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

        public async Task<decimal> GetTotalPriceByCategoryAsync(int categoryId) =>
            await _context.Products.Where(p => p.CategoryId == categoryId).SumAsync(p => p.Price);

        public async Task<Dictionary<string, decimal>> GetTotalPricePerCategoryAsync() =>
            await _context.Categories
                          .Include(c => c.Products)
                          .ToDictionaryAsync(c => c.Name, c => c.Products.Sum(p => p.Price));
    }
}
