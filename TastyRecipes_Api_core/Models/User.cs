using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyRecipes_Api_core.Models
{
    public class User :IdentityUser<int>
    {
      
        
   
        public ICollection<Comments> Comments { get; set; } = new HashSet<Comments>();
        public ICollection<Recipes> Recipes { get; set; } = new HashSet<Recipes>();


    }
}
