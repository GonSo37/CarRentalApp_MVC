using Microsoft.AspNetCore.Identity;

namespace CarRentalApp_MVC.Data
{
    public class SeedData
    {
        public static async Task CreateRoleAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "User", "Manager", "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByEmailAsync("admin@example.com");

            if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
