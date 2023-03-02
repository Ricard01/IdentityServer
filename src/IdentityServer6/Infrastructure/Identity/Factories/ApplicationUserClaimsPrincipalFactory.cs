using IdentityModel;
using IdentityServer6.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityServer6.Infrastructure.Factories;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{

    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {

    }


    // Cuando el usuario se logea se agregan estos claims. 
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user);

        if (user.Nombre != null)
        {
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.Nombre));
        }

        if (user.ApellidoPaterno != null)
        {
            claimsIdentity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.ApellidoPaterno));
        }

        return claimsIdentity;
    }


}
