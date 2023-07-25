using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcommerceAPI.Config
{
    public static class IdentityJwtConfig
    {
        public static IServiceCollection ConfigIdentityOptions(this IServiceCollection service)
        {
            return service.Configure<IdentityOptions>(options =>
             {
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequiredLength = 3;
             });
        }
        public static AuthenticationBuilder ConfigJwtAuthentication(this IServiceCollection service)
        {
            return
                 service
                .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("fdfd34294329849381hfdhajfh@43488*,,|4|&#8/*!nndn")
                            ),
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ClockSkew = TimeSpan.Zero
                        };
                });

        }

    }
}
