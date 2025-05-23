﻿using SmallBizManager.Models;
using System.Collections.Generic;


namespace SmallBizManager.Services
{
    
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct( Product product);
        void DeleteProduct(int id);
    }

}
