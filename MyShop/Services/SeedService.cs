using Microsoft.AspNetCore.Identity;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope=serviceProvider.CreateScope();
            var context= scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var rolemanager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var usermanager=scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger=scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();
            try
            {
                logger.LogInformation("Ensuring the database is ready.");
                await context.Database.EnsureCreatedAsync();

                logger.LogInformation("Seeding Roles");
                await AddRoleAsync(rolemanager, "Admin");
                await AddRoleAsync(rolemanager, "User");

                logger.LogInformation("Seeding Admin data");
                var adminEmail = "admin@myshop.in"; 
                if(await usermanager.FindByEmailAsync(adminEmail)==null)
                {
                    var adminuser = new Users
                    {
                        FullNamne = "Vaibhav Shukla",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var result = await usermanager.CreateAsync(adminuser,"Admin@12345");
                    if(result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin role to the admin user.");
                        await usermanager.AddToRoleAsync(adminuser,"Admin");
                    }
                    else
                    {
                        logger.LogInformation("Failed to create admin user: {Errors}",string.Join(",",result.Errors.Select(e=>e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager,string roleName)
        {
            if(!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if(!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}' : {string.Join(",",result.Errors.Select(e=>e.Description))}");
                }
            }
        }
    }
}
