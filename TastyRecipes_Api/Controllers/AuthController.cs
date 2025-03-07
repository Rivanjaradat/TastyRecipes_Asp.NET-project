using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Interfaces;
using TastyRecipes_Api_core.Models;

namespace TastyRecipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }
        //api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,

            };
            var result = await authRepository.RegisterAsync(user, registerDTO.Password);
            return new JsonResult(result);


        }
        //api/Auth/login
        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await authRepository.LoginAsync(loginDTO.UserName, loginDTO.Password);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { Message = "Invalid Username or Password" });
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }

        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var result = await authRepository.ChangePasswordAsync(changePasswordDTO.Email, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

                if (result == "Password Changed Successfully")
                {
                    return Ok(new { Message = result });
                }

                return BadRequest(new { Message = result }); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during password change: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while changing the password." });
            }
        }

        
       
    }
}
