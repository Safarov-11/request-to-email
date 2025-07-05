using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public static class DefaultRoless
{
    public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<string>()
        {
            "Admin",
            "Mentor",
            "Student"
        };

        foreach (var role in roles)
        {
            var existingRole = await roleManager.FindByNameAsync(role);
            if (existingRole != null)
            {
                continue;
            }
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
