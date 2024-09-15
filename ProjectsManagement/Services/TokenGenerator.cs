using Microsoft.IdentityModel.Tokens;
using ProjectsManagement.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectsManagement.Services
{
    public static class TokenGenerator
    {
        public static string GenerateToken(string id, string name, string roleID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("ID", id),
                    new Claim("RoleID", roleID),
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.Now.AddHours(1),
                Issuer = "ProjectManagement",
                Audience = "ProjectManagement-Users",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
