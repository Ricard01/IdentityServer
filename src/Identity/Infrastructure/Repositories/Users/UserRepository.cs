using Identity.Domain;
using Identity.Infrastructure.Common.Interfaces;
using Identity.Infrastructure.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<Result> CreateAsync(User userRequest)
    {
        var user = new ApplicationUser
        {
            UserName = userRequest.UserName,
            Nombre = userRequest.Nombre,
            ApellidoPaterno = userRequest.ApellidoPaterno,
            Email = userRequest.Email,

            //CreatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userRequest.Password);



        return result.ToApplicationResult();
    }


}
