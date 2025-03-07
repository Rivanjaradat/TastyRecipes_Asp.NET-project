using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api_core.Interfaces
{
   public interface IRecipesRepository
    {
        Task<IEnumerable<object>> GetRecipes();
        Task<string> AddRecipe(RecipesDTO recipe, int userId);
        Task<string> deleteRecipe(int userId, int recipe_id);
        Task<IEnumerable<object>> GetRecipesByUserId(int userId);
    }
}
