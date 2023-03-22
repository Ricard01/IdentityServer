// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Security.Claims;

using IdentityServer6.Infrastructure.Identity;
using IdentityServer6.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Authorize;

/// <summary>
/// This version provides:
/// - Adds Permissions to the user's claims.
/// </summary>
// Thanks to https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims
public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<ApplicationUser>
{
    private readonly ApplicationDbContext _AuthDbContext;

    public AddPermissionsToUserClaims(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor,
        ApplicationDbContext extraAuthDbContext)
        : base(userManager, optionsAccessor)
    {
        _AuthDbContext = extraAuthDbContext;
    }

    //protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    //{
    //    var identity = await base.GenerateClaimsAsync(user);
    //    //var userId = identity.Claims.GetUserIdFromClaims();
    //    //var rtoPCalcer = new CalcAllowedPermissions(_AuthDbContext);
    //    identity.AddClaim(new Claim("hola", "hola"));
    //    return identity;
    //}

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("EmployeeNumber", user.Id));
        return identity;
    }
}


