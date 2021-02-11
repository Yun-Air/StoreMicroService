using Microsoft.AspNetCore.Mvc;
using ProductService.DBContexts;
using ProductService.Models;
using ProductService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        private ProductContext _productContext;

        public CategoryController (ICategoryRepository categoryRepository, ProductContext productContext)
        {
            _categoryRepository = categoryRepository;
            _productContext = productContext;
        }
        //GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetCategories();
            return new OkObjectResult(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            return new OkObjectResult(category);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Category category )
        {
            if (CategoryNameExists(category.Name))
            {
                return BadRequest(new { message = "Category Exists" });
            }

            using (var scope = new TransactionScope())

            {
                _categoryRepository.InsertCategory(category);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] Category category)
        {
            if (category != null)
            {
                using (var scope = new TransactionScope())
                {
                    _categoryRepository.UpdateCategory(category);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return new OkResult();
        }

        private bool CategoryNameExists(string name)
        {
            return _productContext.Categories.Any(c => c.Name == name);
        }
    }
}
