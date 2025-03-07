using Microsoft.EntityFrameworkCore;
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
    public class CommentsRepository : ICommentsRepository
    {
        private readonly AppDbContext context;

        public CommentsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddComment(CommentDTO commentDto, int userId )
        {
            if (commentDto == null)
            {
                return "Invalid comment data";
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return "User not found";
            }

            var recipe = await context.Recipes.FirstOrDefaultAsync(x => x.Id == commentDto.RecipeId);
            if (recipe == null)
            {
                return "Recipe not found";
            }

            var newComment = new Comments
            {
                Comment = commentDto.Comment,
                RecipeId = commentDto.RecipeId,
                UserId = userId, 
                CreatedAt = DateTime.Now
            };

            await context.Comments.AddAsync(newComment);
            await context.SaveChangesAsync();

            return "Comment added successfully";
        }

        public async Task<IEnumerable<CommentResponseDTO>> GetCommentsByRecipeId(int recipeId)
        {
            if (recipeId <= 0)
            {
                return Enumerable.Empty<CommentResponseDTO>(); 
            }

            var comments = await context.Comments
                .Where(c => c.RecipeId == recipeId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDTO
                {
                    Id = c.Id,
                    Comment = c.Comment,
                    UserName = c.User.UserName,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return comments;
        }
        public async Task<string> UpdateComment(int commentId, int userId, string updatedComment)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId);
            if (comment == null)
            {
                return "Comment not found or you are not authorized to edit it";
            }

            comment.Comment = updatedComment;
            await context.SaveChangesAsync();

            return "Comment updated successfully";
        }
        public async Task<string> DeleteComment(int commentId, int userId)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId);
            if (comment == null)
            {
                return "Comment not found or you are not authorized to delete it";
            }

            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

            return "Comment deleted successfully";
        }

        public  async Task<IEnumerable<CommentResponseDTO>> GetCommentsByUserId(int UserId)
        {
            if (UserId <= 0)
            {
                return Enumerable.Empty<CommentResponseDTO>();
            }
            var comments = await context.Comments
                .Where(c => c.UserId == UserId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDTO
                {
                    Id = c.Id,
                    Comment = c.Comment,
                 
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
            return comments;


        }
    }
}
