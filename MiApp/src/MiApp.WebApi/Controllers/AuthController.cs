using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Interfaces;
using MiApp.WebApi.Requests;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AuthController(IConfiguration configuration, ITokenService tokenService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var email = _configuration["AuthSettings:Email"];
        var password = _configuration["AuthSettings:Password"];

        if (request.Email != email || request.Password != password)
        {
            return Unauthorized(new { error = "Credenciales invalidas." });
        }

        var token = _tokenService.GenerateToken(request.Email);

        return Ok(new { token });
    }
}
