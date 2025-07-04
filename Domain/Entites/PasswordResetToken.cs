namespace Domain.Entites;

public class PasswordResetToken{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}
