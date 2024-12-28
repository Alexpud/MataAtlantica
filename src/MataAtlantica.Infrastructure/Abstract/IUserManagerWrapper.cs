using MataAtlantica.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace MataAtlantica.Infrastructure.Abstract;
public interface IUserManagerWrapper
{
    UserManager<User> GetUserManager();
}

public class UserManagerWrapper(UserManager<User> userManager) : IUserManagerWrapper
{
    private readonly UserManager<User> _userManager = userManager;

    public UserManager<User> GetUserManager() => _userManager;
}
