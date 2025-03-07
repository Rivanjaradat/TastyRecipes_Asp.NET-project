using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Interfaces;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoriesRepository;
        public CategoriesController(ICategoryRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        ////api/Categories
        //[HttpGet("Add")]
        //public async Task<IActionResult> GetCategories()
        //{
        //    var categories = await categoriesRepository.GetCategories();
        //    return Ok(categories);
        //}
        //api/Categories
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await categoriesRepository.AddCategory(category);
            if (result == null)
            {
                return BadRequest("Category not added");
            }

            return new JsonResult(result);
        }
        //api/Categories/1
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var result = await categoriesRepository.DeleteCategory(id);
            if (result == null)
            {
                return BadRequest("Category not deleted");
            }
            return new JsonResult(result);
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoriesRepository.GetCategories();
            return Ok(categories);
        }
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await categoriesRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await categoriesRepository.UpdateCategory(category, id);
            if (result == null)
            {
                return BadRequest("Category not updated");
            }
            return new JsonResult(result);
        }
    }
}
