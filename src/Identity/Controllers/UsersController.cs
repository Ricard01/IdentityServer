using Identity.Infrastructure.Repositories.Users;
using Identity.Infrastructure.Repositories.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{

    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<UsersVm>> Get()
    {
        var result = await _userRepository.GetAll();
        return result;
    }
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>>Get(string userId)
    {
        return await _userRepository.GetAsync(userId);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]User userRequest)
    {
        //var result = 

        return Ok(await _userRepository.CreateAsync(userRequest));
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult> Patch(string userId, [FromBody] User userRequest)
    {
        var result = await _userRepository.UpdateAsync(userId, userRequest);
        return Ok(result);
    }
}
