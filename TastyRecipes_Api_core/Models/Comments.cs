using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyRecipes_Api_core.Models
{
   public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
       public User User { get; set; }
        public Recipes Recipe { get; set; }
    }
}
