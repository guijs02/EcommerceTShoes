using IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Context
{
    public class SQLServerContext : IdentityDbContext<Usuario>
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }

    }
}
