namespace Identity.Infrastructure.Repositories.Roles.Dtos
{
    public class RolesVm
    {

        public IList<RolDto> Roles { get; set; } = new List<RolDto>();
    }
}
