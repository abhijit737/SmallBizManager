using SmallBizManager.Data;
using SmallBizManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmallBizManager.Services



{


    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context) => _context = context;

        public IEnumerable<Product> GetAllProducts()

        {
            var products = _context.Products.ToList();
            return products;

        }
            

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product updated)
        {
            var product = _context.Products.Find(updated.Id);
            if (product == null) return;

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Description = updated.Description;
            product.Stock = updated.Stock;

            if (!string.IsNullOrEmpty(updated.ImagePath))
            {
                product.ImagePath = updated.ImagePath;
            }

            _context.SaveChanges();
        }

        
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        Product IProductService.GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

       
    }

}
