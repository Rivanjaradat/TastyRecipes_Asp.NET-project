using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyRecipes_Api_core.DTOs;

namespace TastyRecipes_Api_core.Interfaces
{
    public  interface ICommentsRepository
    {
        Task<string> AddComment(CommentDTO commentDto, int userId);
        Task<IEnumerable<CommentResponseDTO>> GetCommentsByRecipeId(int recipeId);
        Task<string> UpdateComment(int commentId, int userId, string updatedComment);
        Task<string> DeleteComment(int commentId, int userId);
        Task<IEnumerable<CommentResponseDTO>> GetCommentsByUserId(int UserId);


    }
}
