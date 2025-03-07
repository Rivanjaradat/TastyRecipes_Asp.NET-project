using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TastyRecipes_Api.HelperFunctions
{
    public class ExtractClaims
    {
        public static int? ExtractUserId(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var JwtToken = tokenHandler.ReadJwtToken(token);
                var userIdClaim = JwtToken.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;

                }
                return null;


            }
            catch (Exception)
            {
                return null;

            }

        }
    }
}
