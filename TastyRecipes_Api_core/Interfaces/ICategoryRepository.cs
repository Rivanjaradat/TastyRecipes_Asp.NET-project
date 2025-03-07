using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api_core.Interfaces
{
   public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetCategories();
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> AddCategory(CategoryDTO category);

        Task<CategoryDTO> UpdateCategory(CategoryDTO category, int id);
        Task<string> DeleteCategory(int id);
    }
}
