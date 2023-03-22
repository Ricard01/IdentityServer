using AuthUtils.Feature;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthUtils.AuthorizeSetup;

/// <summary>
/// This version provides:
/// - Adds Permissions to the user's claims.
/// </summary>
// Thanks to https://www.thereformedprogrammer.net/a-better-way-to-handle-asp-net-core-authorization-six-months-on/#a-simpler-way-to-add-to-the-users-claims
public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<IdentityUser>
{
    private readonly AuthorizeDbContext _extraAuthDbContext;

    public AddPermissionsToUserClaims(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor,
        AuthorizeDbContext extraAuthDbContext)
        : base(userManager, optionsAccessor)
    {
        _extraAuthDbContext = extraAuthDbContext;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var userId = identity.Claims.GetUserIdFromClaims();
        var rtoPCalcer = new CalcAllowedPermissions(_extraAuthDbContext);
        identity.AddClaim(new Claim(PermissionConstants.PackedPermissionClaimType, await rtoPCalcer.CalcPermissionsForUserAsync(userId)));
        return identity;
    }
}

