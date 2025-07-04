using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Entites;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService(
    UserManager<IdentityUser> userManager,
    IConfiguration config,
    IHttpContextAccessor contextAccessor,
    IEmailService emailService) : IAuthenticationService
{
    public async Task<IdentityResult> RegisterAsync(RegisterDTO register)
    {
        var user = new IdentityUser { UserName = register.Username, Email = register.Email };
        var result = await userManager.CreateAsync(user, register.Password);
        return result;
    }

    public async Task<string?> LoginAsync(LoginDTO login)
    {
        var user = await userManager.FindByNameAsync(login.Username);
        if (user == null) return null;

        var result = await userManager.CheckPasswordAsync(user, login.Password);
        return !result
            ? null
            : GenerateJwtToken(user);
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!)
            };

        var secretKey = config["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDTO change)
    {
        var userName = contextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await userManager.FindByNameAsync(userName!);
        if (user == null)
        {
            return false;
        }
        var changePassword = await userManager.ChangePasswordAsync(user, change.OldPassword, change.NewPassword);
        return changePassword.Succeeded;
    }

    public async Task<bool> RequestPasswordResetAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false;
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        await emailService.SendResetPasswordEmailAsync(email, token);

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetDTO reset)
    {
        var user = await userManager.FindByEmailAsync(reset.Email);
        if (user == null)
        {
            return false;
        }

        var result = await userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);
        return result.Succeeded;
    } 

}

