using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyRecipes_Api.HelperFunctions;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Interfaces;

namespace TastyRecipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository recipesRepository;

        public RecipesController(IRecipesRepository recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddRecipe([FromBody] RecipesDTO recipe)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await recipesRepository.AddRecipe(recipe, userId.Value);
                if (result == "Recipe added successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }

        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetRecipes()
        {
            try
            {
                var recipes = await recipesRepository.GetRecipes();
                if (recipes != null)
                {
                    return Ok(recipes);
                }
                return NotFound("No recipes found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteRecipe(int recipe_id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
             {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await recipesRepository.deleteRecipe(userId.Value, recipe_id);
                if (result == "Recipe deleted successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetRecipesByUserId()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var recipes = await recipesRepository.GetRecipesByUserId(userId.Value);
                if (recipes != null)
                {
                    return Ok(recipes);
                }
                return NotFound("No recipes found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
