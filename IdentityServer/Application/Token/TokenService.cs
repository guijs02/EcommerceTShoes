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
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, usuario.UserName),
                new(ClaimTypes.NameIdentifier, usuario.Id),
                new(ClaimTypes.Email, usuario.Email),
                new("loginTimeStmp", DateTime.UtcNow.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TShoesSettings:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var userToken = new JwtSecurityTokenHandler().WriteToken(token);

            return userToken;
        }
    }
}
