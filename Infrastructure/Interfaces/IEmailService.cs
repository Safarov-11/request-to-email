namespace Infrastructure.Interfaces;

public interface IEmailService
{
    Task SendResetPasswordEmailAsync(string toEmail, string token);
}
