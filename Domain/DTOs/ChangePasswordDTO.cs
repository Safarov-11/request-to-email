using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class ChangePasswordDTO
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null!;
}
