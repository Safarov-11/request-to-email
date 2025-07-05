using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public static class DefaultUsers
{
    public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
    {
        var existingUser = await userManager.FindByNameAsync("Admin");
        if (existingUser != null)
        {
            return;
        }

        var user = new IdentityUser()
        {
            UserName = "Admin",
            Email = "Admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "000080864",
            PhoneNumberConfirmed = true,
        };

        await userManager.CreateAsync(user, "12345");
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

