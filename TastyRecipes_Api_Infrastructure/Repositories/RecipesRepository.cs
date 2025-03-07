using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Interfaces;
using TastyRecipes_Api_core.Models;
using TastyRecipes_Api_Infrastructure.Data;

namespace TastyRecipes_Api_Infrastructure.Repositories
{
   public class RecipesRepository : IRecipesRepository
    {
        private readonly AppDbContext appDbContext;

        public RecipesRepository( AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddRecipe(RecipesDTO recipe, int  userId)
        {
            if (recipe == null)
            {
                return "Invalid recipe data";
            }

           
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return "User not found";
            }

        
            var newRecipe = new Recipes
            {
                Name = recipe.Name,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Directions = recipe.Directions,
               
                CategoryId = recipe.CategoryId,
                UserId = userId
            };

            
            await appDbContext.Recipes.AddAsync(newRecipe);
            await appDbContext.SaveChangesAsync();

            return "Recipe added successfully";
        }


      public async Task<string> deleteRecipe(int userId,int recipe_id) {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return "User not found";
            }
            var recipe= await appDbContext.Recipes.FirstOrDefaultAsync(r=>r.Id == recipe_id);
            if (recipe == null)
            {
                return "Recipe not found";
            }
            else
            {
                var result =  appDbContext.Recipes.Remove(recipe);
                
                await appDbContext.SaveChangesAsync();
                return "Recipe deleted successfully";
            }


        }
        public async Task<IEnumerable<object>> GetRecipesByUserId(int userId)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            var recipes = await appDbContext.Recipes
                .Where(r => r.UserId == userId)
                .Include(r => r.Category)
                .Include(r => r.User)
                .Select(r => new
                {
                    RecipeName = r.Name,
                    RecipeDescription = r.Description,
                    Ingredients = r.Ingredients,
                    Directions = r.Directions,
                    CategoryName = r.Category.Name,
                    UserName = r.User.UserName
                })
                .ToListAsync();
            return recipes;

        }
        public async Task<IEnumerable<object>> GetRecipes()
    {
        var recipes = await appDbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.User)
            .Select(r => new
            {
                RecipeName = r.Name,
                RecipeDescription = r.Description,
                Ingredients = r.Ingredients,
                Directions = r.Directions,
                CategoryName = r.Category.Name,
                UserName = r.User.UserName
            })
            .ToListAsync();

        return recipes;
    }


}
}
