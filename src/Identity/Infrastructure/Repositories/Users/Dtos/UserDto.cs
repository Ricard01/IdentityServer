using AutoMapper;
using Identity.Domain;

namespace Identity.Infrastructure.Repositories.Users.Dtos;

[AutoMap(typeof(ApplicationUser))]
public class UserDto
{
    public string? Id { get; set; }

    public string? UserName { get; set; }

    public string? Nombre { get; set; }

}
