using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context; 
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void DeleteProductById(int id)
        {
            var productId= _context.Products.FirstOrDefault(x => x.Id==id);
            if (productId != null)
            {
                _context.Products.Remove(productId);

            }
            _context.SaveChanges();
           
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();   
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id ==id);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct == null) throw new ArgumentNullException(nameof(updatedProduct));

            var product = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product == null) throw new InvalidOperationException($"Product with ID {updatedProduct.Id} not found.");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId;
            product.Description = updatedProduct.Description;
            product.UpdatedAt = DateTime.Now;
            
        }
    }
}
