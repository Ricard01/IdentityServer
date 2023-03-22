using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AuthDomain.Entities;


/// <summary>
/// This is a one-to-many relationship between the User (represented by the UserId) and their Roles (represented by RoleToPermissions)
/// </summary>
public class UserToRole //: IAddRemoveEffectsUser, IChangeEffectsUser
{
    //private UserToRokle() { } //needed by EF Core

    public UserToRole(string userId, RoleToPermissions role)
    {
        UserId = userId;      
        Role = role;
    }

    //I use a composite key for this table: combination of UserId and RoleName
    //That has to be defined by EF Core's fluent API
    [Required(AllowEmptyStrings = false)]
    [MaxLength(AuthConstants.UserIdSize)]
    public string UserId { get; private set; }

    [MaxLength(AuthConstants.RoleNameSize)]
    public string? RoleName { get; private set; }

    [ForeignKey(nameof(RoleName))]
    public RoleToPermissions Role { get; private set; }


}
