using Microsoft.EntityFrameworkCore;
using ProductService.DBContexts;
using ProductService.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public void DeleteProduct(int ProductId)
        {
            var product = _productContext.Products.Find(ProductId);
            _productContext.Products.Remove(product);
            Save();
        }


        public Product GetProductByID(int ProductId)
        {
            return _productContext.Products.Find(ProductId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productContext.Products.ToList();
        }

        public void InsertProduct(Product product)
        {
            _productContext.Add(product);
            Save();
        }

        public void Save()
        {
            _productContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _productContext.Entry(product).State = EntityState.Modified;
            Save();
        }
    }
}


