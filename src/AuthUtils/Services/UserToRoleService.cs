using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthUtils.Services;

public class UserToRoleService
{
    public static IStatusGeneric<UserToRole> AddRoleToUser(string userId, string roleName, AuthorizeDbContext context)
    {
        if (roleName == null) throw new ArgumentNullException(nameof(roleName));

        var status = new StatusGenericHandler<UserToRole>();
        if (context.Find<UserToRole>(userId, roleName) != null)
        {
            status.AddError($"The user already has the Role '{roleName}'.");
            return status;
        }
        var roleToAdd = context.Find<RoleToPermissions>(roleName);
        if (roleToAdd == null)
        {
            status.AddError($"I could not find the Role '{roleName}'.");
            return status;
        }

        return status.SetResult(new UserToRole(userId, roleToAdd));
    }

}
