using Domain.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var result = await service.RegisterAsync(dto);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var token = await service.LoginAsync(dto);
        if (token == null) return Unauthorized();
        return Ok(new { token });
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset(string email)
    {
        var result = await service.RequestPasswordResetAsync(email);
        if (!result)
            return NotFound("User with this email not found");
        return Ok("Reset email sent");
    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> ResetPassword(ResetDTO reset)
    {
        var result = await service.ResetPasswordAsync(reset);
        if (!result)
            return BadRequest("Invalid token or password");
        return Ok("Password ha been reset");
    }

    [Authorize]
    [HttpPost("Change")]
    public async Task<IActionResult> Change(ChangePasswordDTO dto)
    {
        var result = await service.ChangePasswordAsync(dto);
        if (result == false)
            return BadRequest();
        return Ok("Password changed successfully");
    }

}
