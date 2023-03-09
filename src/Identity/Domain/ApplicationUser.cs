using Microsoft.AspNetCore.Identity;

namespace Identity.Domain;

public class ApplicationUser : IdentityUser
{
    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

}
