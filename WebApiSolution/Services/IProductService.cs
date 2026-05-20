using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public interface IProductService
    {
        public void AddProduct(Product product);
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public void DeleteProductById(int id);  
        public void UpdateProduct(Product product); 

    }
}
