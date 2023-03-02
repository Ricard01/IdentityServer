using Microsoft.AspNetCore.Identity;

namespace IdentityServer6.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    // IdentityUser desconoce estos campos por lo tanto no van a estar incluido en los claims  asi que se agregan a través de claimsprincpalFactory

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }
}
