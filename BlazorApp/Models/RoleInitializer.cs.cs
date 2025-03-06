using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace YourAppNamespace.Identity
{
    // Replace 'ApplicationUser' with your actual Identity user class if it's named differently.
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Get RoleManager and UserManager from the dependency injection container.
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define the roles you want to create.
            string[] roles = { "mainadmin", "admin" };

            // Create each role if it does not already exist.
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Optionally create the main admin account if it does not exist.
            // Use a secure email and password in a production environment.
            string mainAdminEmail = "mainadmin@example.com";
            var mainAdminUser = await userManager.FindByEmailAsync(mainAdminEmail);
            if (mainAdminUser == null)
            {
                var mainAdmin = new ApplicationUser
                {
                    UserName = mainAdminEmail,
                    Email = mainAdminEmail
                };

                var result = await userManager.CreateAsync(mainAdmin, "YourSecurePassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mainAdmin, "mainadmin");
                }
                else
                {
                    // Handle errors as needed (e.g., log them or throw an exception)
                    throw new Exception("Failed to create the main admin account.");
                }
            }
        }
    }
}
