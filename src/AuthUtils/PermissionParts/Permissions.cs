using System.ComponentModel.DataAnnotations;

namespace AuthUtils.PermissionParts;

public  enum Permissions :short
{

    NotSet = 0, //error condition

    //Here is an example of very detailed control over something
    [Display(GroupName = "Users", Name = "Acceso", Description = "Puede Ver la lista de usuarios o un usuario.")]
    UserGet = 10,

    [Display(GroupName = "Users", Name = "Crear", Description = "Puede Crear usuarios")]
    UserCreate = 13,

    [Display(GroupName = "Users", Name = "Eliminar", Description = "Puede Eliminar usuarios")]
    UserDelete = 14,

    [Display(GroupName = "Users", Name = "Editar", Description = "Puede Editar usuarios")]
    UserPatch = 15,

    [LinkedToModule(PaidForModules.Feature1)]
    [Display(GroupName = "Features", Name = "Feature1", Description = "Can access feature1")]
    Feature1Access = 1000,

    [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
    AccessAll = Int16.MaxValue,


}

public enum PermissionOperator
{
    And = 1,
    Or = 2
}
