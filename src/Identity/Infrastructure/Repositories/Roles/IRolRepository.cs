using Identity.Infrastructure.Common.Models;
using Identity.Infrastructure.Repositories.Roles.Dtos;
using Identity.Infrastructure.Repositories.Roles.Models;

namespace Identity.Infrastructure.Repositories.Roles;

public interface IRolRepository
{

    /// <summary>
    /// List of roles
    /// </summary>
    /// <returns>List of all the roles.</returns>
    Task<RolesVm> GetAll();

    ///<summary> Get a single rol by Id </summary>
    ///<remarks> Gets a single rol by Id with his specific users</remarks>
    ///<returns>Single user with roles</returns>
    Task<RolDto> GetAsync(string roleId);

    /// <summary>
    /// List of permissions
    /// </summary>
    /// <returns>List of all the permissions.</returns>
    //Task<List<ApplicationPermission>> GetPermissions();

    /// <summary>
    /// Creates the role with claims
    /// </summary>
    /// <returns>The result from and identityResult operation </returns>
    Task<Result> CreateAsync(Rol roleRequest);

    /// <summary>
    /// Updates the roles name
    /// </summary>
    /// <returns>The result from and identityResult operation </returns>
    //Task<Result> UpdateAsync(string roleId, Role roleRequest);

    /// <summary>
    /// Deletes the role by id
    /// </summary>
    /// <returns>The result from and identityResult operation </returns>
    //Task<Result> DeleteAsync(string roleId);


}
