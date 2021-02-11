using Microsoft.EntityFrameworkCore;
using ProductService.DBContexts;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ProductContext _productContext;

        public CategoryRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public void DeleteCategory(int CategoryId)
        {
            var category = _productContext.Categories.Find(CategoryId);
            _productContext.Categories.Remove(category);
            Save();
        }


        public Category GetCategoryById(int CategoryId)
        {
            return _productContext.Categories.Find(CategoryId);

        }
        //public Category GetCategoryByName( string CategoryName)
        //{
        //    return _productContext.Categories.Find(CategoryName);
        //}
        public IEnumerable<Category> GetCategories(string CategoryName)
        {
            //var query = _productContext.Categories.AsNoTracking().AsQueryable();


            if (!String.IsNullOrEmpty(CategoryName))
            {

               var query = _productContext.Categories.AsNoTracking().AsQueryable().Where(Category => Category.Name.ToLower().Equals(CategoryName.ToLower()));
                 return query.ToList();
            }
            return _productContext.Categories.ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _productContext.Categories.ToList();
        }
        public void InsertCategory(Category category)
        {
            _productContext.Add(category);
            Save();
        }

        public void Save()
        {
           _productContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
           _productContext.Entry(category).State = EntityState.Modified;
           Save();
        }

    }
}

