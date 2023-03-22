using AuthDomain.Entities;
using AuthUtils.Intefaces;
using AuthUtils.PermissionParts;
using StatusGeneric;
using System.ComponentModel.DataAnnotations;

namespace AuthUtils.Services
{
    public  class RoleToPermissionsService : IRoleToPermissions
    {

        [Required] //A role must have at least one role in it
        private string _permissionsInRole;

        /// <summary>
        /// This returns the list of permissions in this role
        /// </summary>
        public IEnumerable<Permissions> PermissionsInRole => _permissionsInRole.UnpackPermissionsFromString();

        public static IStatusGeneric<RoleToPermissions> CreateRoleWithPermissions(string roleName, string description, ICollection<Permissions> permissionInRole,
            AuthorizeDbContext context)
        {
            var status = new StatusGenericHandler<RoleToPermissions>();
            if (context.Find<RoleToPermissions>(roleName) != null)
            {
                status.AddError("That role already exists");
                return status;
            }

            return status.SetResult(new RoleToPermissions(roleName, description, permissionInRole));
        }

        public void Update(string description, ICollection<Permissions> permissions)
        {
            if (permissions == null || !permissions.Any())
                throw new InvalidOperationException("There should be at least one permission associated with a role.");

            _permissionsInRole = permissions.PackPermissionsIntoString();
            Description = description;
        }

        public IStatusGeneric DeleteRole(string roleName, bool removeFromUsers,
            AuthorizeDbContext context)
        {
            var status = new StatusGenericHandler { Message = "Deleted role successfully." };
            var roleToUpdate = context.Find<RoleToPermissions>(roleName);
            if (roleToUpdate == null)
                return status.AddError("That role doesn't exists");

            var usersWithRoles = context.UserToRoles.Where(x => x.RoleName == roleName).ToList();
            if (usersWithRoles.Any())
            {
                if (!removeFromUsers)
                    return status.AddError($"That role is used by {usersWithRoles.Count} and you didn't ask for them to be updated.");

                context.RemoveRange(usersWithRoles);
                status.Message = $"Removed role from {usersWithRoles.Count} user and then deleted role successfully.";
            }

            context.Remove(roleToUpdate);
            return status;
        }
    }
}
