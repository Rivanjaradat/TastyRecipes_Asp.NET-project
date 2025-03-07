using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyRecipes_Api_core.DTOs
{
    public  class RecipesDTO
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public string Directions { get; set; }
       // public string Image { get; set; }
        public int CategoryId{ get; set; }
      
    }
}
