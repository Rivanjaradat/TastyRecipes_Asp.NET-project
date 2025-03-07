using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyRecipes_Api_core.DTOs
{
  public class ConfirmEmailDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
