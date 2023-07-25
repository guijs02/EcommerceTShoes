using LoginAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Context
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
        {
        }
    }
}
