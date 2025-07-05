using System.Net;
using System.Net.Mail;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class EmailService(IConfiguration config) : IEmailService
{
    public async Task SendResetPasswordEmailAsync(string toEmail, string token)
    {
        var fromEmail = "romasafarov.ru@gmail.com"; //az kadom pochta ravon meshava
        var fromPassword = config["EmailSettingPassword"]; //kodi 16raqama
        var subject = "Reset Password"; //temai email
        var body = $"your code for reseting password: {token}"; //texti email

        var smtpClient = new SmtpClient 
        {
            Host = "smtp.gmail.com",
            Port = 587, //
            EnableSsl = true, //baroi soedinenra shifr kardan (obyazatelno!)
            Credentials = new NetworkCredential(fromEmail, fromPassword) //aftorizatsiya
        };

        using var messenge = new MailMessage(fromEmail, toEmail, subject, body);
        await smtpClient.SendMailAsync(messenge);

    }
}

