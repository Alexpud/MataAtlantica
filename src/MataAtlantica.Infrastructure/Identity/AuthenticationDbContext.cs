using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.Infrastructure.Identity;

public class AuthenticationDbContext : IdentityDbContext<User>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        base.OnModelCreating(builder);
    }
}
