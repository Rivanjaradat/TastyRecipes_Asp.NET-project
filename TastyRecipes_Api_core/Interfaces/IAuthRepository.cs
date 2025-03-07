using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api_core.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(User user, string password);
        Task<string> LoginAsync(string username, string password);
        Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword);
       
    }
}
