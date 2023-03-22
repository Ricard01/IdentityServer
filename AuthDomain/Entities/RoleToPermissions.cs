using System.ComponentModel.DataAnnotations;

namespace AuthDomain.Entities;


/// <summary>
/// This holds each Roles, which are mapped to Permissions
/// </summary>
public class RoleToPermissions// : IChangeEffectsUser
{
    private ICollection<string> permissionInRole;

    //public RoleToPermissions(string roleName, string description, ICollection<AuthUtils.PermissionParts.Permissions> permissionInRole)
    //{
    //    RoleName = roleName;
    //    Description = description;
    //    this.permissionInRole = permissionInRole;
    //}
    private RoleToPermissions(string roleName, string description, ICollection<string> permissions)
    {
        RoleName = roleName;
        Description = description;
        permissionInRole = permissions;
        //Update(description, permissions);
    }



    //private RoleToPerkcmissions() { }

    /// <summary>
    /// This creates the Role with its permissions
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="description"></param>
    /// <param name="permissions"></param>
    //private RoleToPermissions(string roleName, string description, ICollection<Permissions> permissions)
    //{

    //    RoleName = roleName;
    //    _permissionsInRole = permissions.PackPermissionsIntoString();
    //    Update(description, permissions);
    //}

    /// <summary>
    /// ShortName of the role
    /// </summary>
    [Key]
    [Required(AllowEmptyStrings = false)]
    [MaxLength(AuthConstants.RoleNameSize)]
    public string? RoleName { get; private set; }

    /// <summary>
    /// A human-friendly description of what the Role does
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string? Description { get; private set; }



}

