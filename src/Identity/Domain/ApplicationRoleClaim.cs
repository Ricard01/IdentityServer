//using Identity.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.RoleClaim;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public virtual IdentityRole? Role { get; set; }
}
