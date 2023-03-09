using Identity.Infrastructure.Repositories.Users;
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

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]User userRequest)
    {
        var result = await _userRepository.CreateAsync(userRequest);

        return Ok(result);
    }
}
