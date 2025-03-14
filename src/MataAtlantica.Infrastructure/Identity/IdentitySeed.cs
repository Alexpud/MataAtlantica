using MataAtlantica.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MataAtlantica.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task Seed(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserIdentityService>();
        await userManager.AdicionarUsuario("admin@mataatlantica.com", "123456");
    }
}