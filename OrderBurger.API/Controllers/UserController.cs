using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBurger.API.DTOs;
using OrderBurger.API.Services;

namespace OrderBurger.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[AllowAnonymous]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, CancellationToken cancellationToken)
    {
        var result = await _userService.LoginAsync(request, cancellationToken);
        return result is null
            ? Unauthorized(new { message = "E-mail ou senha inválidos." })
            : Ok(result);
    }
}
