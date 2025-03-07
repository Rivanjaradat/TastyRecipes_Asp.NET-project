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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<CategoryDTO> AddCategory(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }

          
            var categoryEntity = new Categories
            {
                Name = category.Name,
                Description = category.Description
               
            };

            await appDbContext.Categories.AddAsync(categoryEntity);
            await appDbContext.SaveChangesAsync();

            
            var categoryDTO = new CategoryDTO
            {
                Name = categoryEntity.Name,
                Description = categoryEntity.Description
            };

            return categoryDTO;
        }



        public  async Task<string> DeleteCategory(int id)
        {
            if (id <= 0)
            {
               return"Category Id cannot be null";
            }
            //
         var category=   await appDbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return "Category not found";
            }
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return "Category deleted sucssesful";



        }

        public Task<IEnumerable<Categories>> GetCategories()
        {
            var categories = appDbContext.Categories.AsEnumerable();
            return Task.FromResult(categories);



        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            var category = await appDbContext.Categories.FindAsync(id);
            if (category == null)
            {
               
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            var categoryDTO = new CategoryDTO
            {
                Name = category.Name,
                Description = category.Description
            };

            return categoryDTO;
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO category, int id)
        {
            if (category == null)
            { 
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }

            var categoryEntity = await appDbContext.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

          
            categoryEntity.Name = category.Name;
            categoryEntity.Description = category.Description;

            await appDbContext.SaveChangesAsync();

            var updatedCategoryDTO = new CategoryDTO
            {
                Name = categoryEntity.Name,
                Description = categoryEntity.Description
            };

            return updatedCategoryDTO;
        }

    }
}
