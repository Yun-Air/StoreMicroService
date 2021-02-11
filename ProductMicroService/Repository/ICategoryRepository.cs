using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories( string CategoryName);
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int CategoryId);
        //Category GetCategoryByName(string CategoryName);
        void InsertCategory(Category category);
        void DeleteCategory(int CategoryId);
        void UpdateCategory(Category category);

        void Save();
    }
}
