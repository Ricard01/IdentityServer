using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Infrastructure.Common.Exceptions;
using Identity.Infrastructure.Common.Models;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories.Roles.Dtos;
using Identity.Infrastructure.Repositories.Roles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Identity.Infrastructure.Repositories.Roles;


/// <inheritdoc />
public class RolRepository : IRolRepository
{
    //private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public RolRepository (RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        //_context = context;
        _roleManager = roleManager;
        _mapper = mapper;
    }


    public async Task<RolesVm> GetAll()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return new RolesVm
        {
            Roles = await _roleManager.Roles
            .AsNoTracking()
            .ProjectTo<RolDto>(_mapper.ConfigurationProvider)
            .OrderBy(r => r.RolName)
            .ToListAsync()
        };
    }


    public async Task<RolDto> GetAsync(string roleId)
    {
        var rol = await _roleManager.Roles.ProjectTo<RolDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(u => u.Id == roleId);

        if (rol == null)
        {
            throw new NotFoundException(nameof(IdentityRole), roleId);
        }
        return rol;
    }


    //public async Task<List<ApplicationPermission>> GetPermissions()
    //{
    //    return await _context.ApiPermissions.ToListAsync();
    //}


    public async Task<Result> CreateAsync(Rol roleRequest)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleRequest.RolName });

        if (result.Succeeded)
        {
            var role = await _roleManager.FindByNameAsync(roleRequest.RolName);

            //if (role != null)
            //{
            //    foreach (var claim in roleRequest.RoleClaims)
            //    {
            //        await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));

            //    }
            //}

        }

        return result.ToApplicationResult();

    }


    public async Task<Result> UpdateAsync(string roleId, Rol roleRequest)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
        {
            throw new NotFoundException(nameof(IdentityRole), roleId);
        }

        role.Name = roleRequest.RolName;

        var result = await _roleManager.UpdateAsync(role);

        //if (result.Succeeded)
        //{
        //    result = await UpdateClaimsAsync(role, roleRequest.RoleClaims);
        //}

        return result.ToApplicationResult();

    }


    //private async Task<IdentityResult> UpdateClaimsAsync(ApplicationRole role, List<RoleClaim> roleClaims)
    //{
    //    var currentClaims = await _roleManager.GetClaimsAsync(role);

    //    // By default result is false
    //    IdentityResult result = new IdentityResult();

    //    // compare list to find if there is claims to Add or Delete
    //    var claimsToAdd = roleClaims.Where(rc => currentClaims.All(cc => cc.Type == rc.ClaimType && cc.Value != rc.ClaimValue)).ToList();

    //    var claimsToDelete = currentClaims.Where(cc => roleClaims.All(rc => rc.ClaimType == cc.Type && rc.ClaimValue != cc.Value)).ToList();


    //    foreach (var claim in claimsToAdd)
    //    {
    //        result = await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));

    //        if (!result.Succeeded) { return result; }

    //    }

    //    foreach (var claim in claimsToDelete)
    //    {
    //        result = await _roleManager.RemoveClaimAsync(role, claim);

    //        if (!result.Succeeded) { return result; }
    //    }


    //    // if its here everithing when well
    //    result = IdentityResult.Success;
    //    return result;

    //}


    //public async Task<Result> DeleteAsync(string roleId)
    //{
    //    var role = _roleManager.Roles.SingleOrDefault(u => u.Id == roleId);

    //    if (role == null)
    //    {
    //        var result = IdentityResult.Failed(new IdentityError { Description = $"Role with id {roleId} not Found" });
    //        return result.ToApplicationResult();
    //    }

    //    var rolInAnyUser = _context.ApplicationUserRoles.Any(ur => ur.RoleId == roleId);

    //    if (rolInAnyUser)
    //    {
    //        var result = IdentityResult.Failed(new IdentityError { Description = $"Role {role.Name} has users" });
    //        return result.ToApplicationResult();
    //    }

    //    return await DeleteAsync(role);
    //}


    //private async Task<Result> DeleteAsync(ApplicationRole role)
    //{
    //    var result = await _roleManager.DeleteAsync(role);

    //    return result.ToApplicationResult();
    //}

}

