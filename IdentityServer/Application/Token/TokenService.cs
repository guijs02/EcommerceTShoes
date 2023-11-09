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

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim("loginTimeStmp", DateTime.Now.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TShoesSettings:SecretKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
