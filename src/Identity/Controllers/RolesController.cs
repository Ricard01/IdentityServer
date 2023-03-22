using Identity.Infrastructure.Repositories.Roles;
using Identity.Infrastructure.Repositories.Roles.Dtos;
using Identity.Infrastructure.Repositories.Roles.Models;
using Identity.Infrastructure.Repositories.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RolesController : ControllerBase
{

    private readonly IRolRepository _rolRepository;

    public RolesController(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }


    [HttpGet]
    public async Task<ActionResult<RolesVm>> Get()
    {
        var result = await _rolRepository.GetAll();
        return result;
    }
    [HttpGet("{rolId}")]
    public async Task<ActionResult<RolDto>> Get(string rolId)
    {
        return await _rolRepository.GetAsync(rolId);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Rol rolRequest)
    {
        //var result = 

        return Ok(await _rolRepository.CreateAsync(rolRequest));
    }
}
