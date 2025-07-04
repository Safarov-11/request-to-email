using Domain.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces;

public interface IAuthenticationService
{
    public Task<IdentityResult> RegisterAsync(RegisterDTO register);
    public Task<string?> LoginAsync(LoginDTO login);
    public Task<bool> ResetPasswordAsync(ResetDTO reset);
    public Task<bool> ChangePasswordAsync(ChangePasswordDTO change);
    Task<bool> RequestPasswordResetAsync(string email);
}
