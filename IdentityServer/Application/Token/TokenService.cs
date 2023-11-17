using IdentityServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServerAPI.Application.Token
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TShoesSettings:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                 new Claim[]
                 {
                        new Claim(ClaimTypes.Name, usuario.UserName),
                        new Claim("id", usuario.Id),
                        new Claim("loginTimeStmp", DateTime.UtcNow.ToString()),
                 }
             ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
