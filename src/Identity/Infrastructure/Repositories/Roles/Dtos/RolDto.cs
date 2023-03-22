using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Repositories.Roles.Dtos
{
    [AutoMap(typeof(IdentityRole))]
    public class RolDto
    {

        //public RoleDto()
        //{
        //    RoleClaims = new List<RoleClaimDto>();

        //}

        public string? Id { get; set; }

        [SourceMember(nameof(IdentityRole.Name))]
        public string? RolName { get; set; }

        //public IList<RoleClaimDto> RoleClaims { get; set; }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<IdentityRole, RolDto>()
        //    .ForMember(r => r.RolName, opt => opt.MapFrom(r => r.Name));

        //}
    }
}
