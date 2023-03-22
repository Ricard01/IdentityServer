using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain;
using Identity.Infrastructure.Common.Exceptions;
using Identity.Infrastructure.Common.Interfaces;
using Identity.Infrastructure.Common.Models;
using Identity.Infrastructure.Repositories.Users.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserRepository(ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result> CreateAsync(User userRequest)
    {
        var user = new ApplicationUser
        {
            UserName = userRequest.UserName,
            Nombre = userRequest.Nombre,
            ApellidoPaterno = userRequest.ApellidoPaterno,
            Email = userRequest.Email,

            //CreatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userRequest.Password);



        return result.ToApplicationResult();
    }


    public async Task<Result> UpdateAsync(string userId, User userRequest)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), userId);
        }

        user.Nombre = userRequest.Nombre;
        user.ApellidoPaterno = userRequest.ApellidoPaterno;
        user.Email = userRequest.Email;




        var result = await _userManager.UpdateAsync(user);

        return result.ToApplicationResult();
        //if (result.Succeeded)
        //{

        //    result = await UpdateRolesAsync(user, userRequest.UserRoles);
        //}



    }

    public async Task<UsersVm> GetAll()
    {
        return new UsersVm
        {
            Users = await _userManager.Users
             .AsNoTracking()
             .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
             .OrderBy(u => u.UserName)
             .ToListAsync()
        };

    }


    public async Task<UserDto> GetAsync(string userId)
    {
        var user = await _userManager.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
         
            throw new NotFoundException(nameof(ApplicationUser), userId);
        }

        return user;
    }


    //public async Task<Result> CreateAsync(User userRequest)
    //{

    //    // Maybe too much but the AddToRoleAsync doesnt return any error. 
    //    var resultValidation = await validateRoles(userRequest.UserRoles);

    //    if (!resultValidation.Succeeded)
    //    {
    //        return resultValidation.ToApplicationResult();
    //    }

    //    var user = new ApplicationUser
    //    {
    //        UserName = userRequest.UserName,
    //        Name = userRequest.Name,
    //        Email = userRequest.Email,
    //        PhoneNumber = userRequest.PhoneNumber,
    //        ProfilePictureUrl = userRequest.ProfilePictureUrl,
    //        CreatedAt = DateTime.Now

    //    };

    //    var result = await _userManager.CreateAsync(user, userRequest.Password);

    //    if (result.Succeeded)
    //    {
    //        foreach (var rol in userRequest.UserRoles)
    //        {

    //            // doest return error if roleName doesnt exists
    //            await _userManager.AddToRoleAsync(user, rol.RoleName);
    //        }

    //        return result.ToApplicationResult(user, userRequest.UserRoles.First().RoleName);
    //    }

    //    return result.ToApplicationResult();

    //}




    //private async Task<IdentityResult> UpdateRolesAsync(ApplicationUser user, List<Rol> userRoles)
    //{
    //    var currentRoles = await _userManager.GetRolesAsync(user);

    //    // By default result is false
    //    IdentityResult result = new IdentityResult();

    //    result = await validateRoles(userRoles);

    //    // If there was and error returns;
    //    if (!result.Succeeded)
    //    {
    //        return result;
    //    }

    //    currentRoles = currentRoles.Select(cr => cr.ToLower()).ToList();

    //    // compare list to find if there is roles to Add or Delete
    //    var rolesToAdd = userRoles.Where(r => currentRoles.All(cr => cr != r.RoleName.ToLower())).ToList();

    //    var rolesToDelete = currentRoles.Where(cr => userRoles.All(r => r.RoleName.ToLower() != cr)).ToList();


    //    // first rolesAdd cause i dont want to delete later and not having any rol 
    //    foreach (var rol in rolesToAdd)
    //    {
    //        result = await _userManager.AddToRoleAsync(user, rol.RoleName);

    //        // if error stops 
    //        if (!result.Succeeded) { return result; }

    //    }


    //    foreach (var role in rolesToDelete)
    //    {
    //        result = await _userManager.RemoveFromRoleAsync(user, role);

    //        // if error stops 
    //        if (!result.Succeeded) { return result; }
    //    }

    //    // it could be that there isnt roles to Add or delete cause the last result is true (validateRoles) returns success.
    //    // if its here everithing when well
    //    return result;

    //}


    //private async Task<IdentityResult> validateRoles(List<Rol> userRoles)
    //{

    //    IdentityResult result = new IdentityResult();

    //    List<Rol> inValidRoles = new List<Rol>();

    //    if (userRoles?.Any() != true)
    //    {
    //        result = IdentityResult.Failed(new IdentityError { Description = "No role was specified " });
    //        return result;
    //    }


    //    foreach (var rol in userRoles)
    //    {

    //        var roleExists = await _roleManager.RoleExistsAsync(rol.RoleName);

    //        if (!roleExists)
    //        {

    //            // TODO should i throw error or Result  throw error new NotFoundException(nameof(ApplicationRole), rol.RoleName);
    //            inValidRoles.Add(new Rol { RoleName = rol.RoleName });

    //        }

    //    }

    //    if (inValidRoles.Any())
    //    {

    //        var identityError = inValidRoles.Select(x => new IdentityError() { Description = $"Role {x.RoleName} doesn't exists" }).ToList();

    //        result = IdentityResult.Failed(identityError.ToArray());

    //        return result;
    //    }

    //    // IF its here there was no error;
    //    result = IdentityResult.Success;

    //    return result;
    //}


    //public async Task<Result> DeleteAsync(string userId)
    //{
    //    if (_currentUserService.UserId == userId)
    //    {
    //        return IdentityResult.Failed(new IdentityError { Description = $"Can't delete the user that you are currently logged in" }).ToApplicationResult(); ;
    //    }
    //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //    if (user == null)
    //    {
    //        var result = IdentityResult.Failed(new IdentityError { Description = $"User with id {userId} not Found" });
    //        return result.ToApplicationResult();
    //    }

    //    return await DeleteAsync(user);
    //}


    //private async Task<Result> DeleteAsync(ApplicationUser user)
    //{
    //    var result = await _userManager.DeleteAsync(user);

    //    return result.ToApplicationResult();
    //}


}
