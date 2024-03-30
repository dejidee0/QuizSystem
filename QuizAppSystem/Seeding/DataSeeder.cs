using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using QuizAppSystem.Models; 

namespace QuizAppSystem.Seeding
{
    public static class DataSeeder
    {
        public static async Task InitializeRolesAndAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                // Create roles if they don't exist
                await CreateRoleIfNotExists(roleManager, "Admin");
                await CreateRoleIfNotExists(roleManager, "Examiner");
                await CreateRoleIfNotExists(roleManager, "Participants");

                // Create admin user if not exists
                var adminEmail = "Ifemicheal@gmail.com"; 
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        FirstName = "Admin",
                        LastName = "User",
                        
                    };

                    var result = await userManager.CreateAsync(adminUser, "Mike0@"); 

                    if (result.Succeeded)
                    {
                        
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                       
                        throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
