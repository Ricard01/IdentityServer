namespace Identity.Domain.Permissions;

public class ApplicationPermission
{
    public int Id { get; set; }

    // This is equal to the controller 
    public string? Name { get; set; }

    // this is equal to the Api Name     
    public string? Module { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
