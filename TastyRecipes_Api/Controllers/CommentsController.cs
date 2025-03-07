using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyRecipes_Api.HelperFunctions;
using TastyRecipes_Api_core.DTOs;
using TastyRecipes_Api_core.Interfaces;
using TastyRecipes_Api_Infrastructure.Data;

namespace TastyRecipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly AppDbContext context;

        public CommentsController(ICommentsRepository commentsRepository,AppDbContext context)
        {
            _commentsRepository = commentsRepository;
            this.context = context;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Invalid comment data");
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await _commentsRepository.AddComment(commentDto, userId.Value);

                if (result == "User not found" || result == "Recipe not found")
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetByRecipeId")]
       
        public async Task<IActionResult> GetCommentsByRecipeId(int recipeId)
        {
            
            var recipeExists = await context.Recipes.AnyAsync(r => r.Id == recipeId);
            if (!recipeExists)
            {
                return NotFound("Recipe not found.");
            }

            var comments = await _commentsRepository.GetCommentsByRecipeId(recipeId);

            if (!comments.Any())
            {
                return NotFound("No comments found for this recipe.");
            }

            return Ok(comments);
        }
        [HttpGet("GetByUserId")]

        public async Task<IActionResult> GetCommentsUserId()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var comments = await _commentsRepository.GetCommentsByUserId(userId.Value);
                if (comments != null)
                {
                    return Ok(comments);
                }
                return NotFound("No comments found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            [HttpPut("Update")]
        public async Task<IActionResult> UpdateComment(int commentId, string updatedComment)
        {
            if (commentId <= 0 || string.IsNullOrEmpty(updatedComment))
            {
                return BadRequest("Invalid comment data");
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await _commentsRepository.UpdateComment(commentId, userId.Value, updatedComment);
                if (result == "Comment not found")
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (commentId <= 0)
            {
                return BadRequest("Invalid comment data");
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            try
            {
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await _commentsRepository.DeleteComment(commentId, userId.Value);
                if (result == "Comment not found")
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
