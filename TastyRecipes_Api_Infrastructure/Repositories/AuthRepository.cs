using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.Interfaces;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api_Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManger;
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManger = signInManger;
            this.configuration = configuration;
        }
        public async Task<string> RegisterAsync(User user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return "User Registered successfully";
            }
            var errorMessage = result.Errors.Select(error => error.Description).ToList();
            return string.Join(",", errorMessage);

        }
        public Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return Task.FromResult("User not found");
            }
            var result = userManager.ChangePasswordAsync(user, oldPassword, newPassword).Result;
            if (result.Succeeded)
            {
                return Task.FromResult("Password Changed Successfully");
            }
            var errorMessage = result.Errors.Select(error => error.Description).ToList();
            return Task.FromResult(string.Join(",", errorMessage));

        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return "Invalid Username or Password";
            }
            var result = await signInManger.PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

         var token= GenerateToken(user);
            return token;
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
             
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

     

    }
}
