using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seed
{
    public class SeedData
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public SeedData(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAndUsersAsync()
        {
            try
            {
                // Check if the database has already been seeded
                if (await _roleManager.Roles.AnyAsync())
                {
                    return;
                }

                // Create roles
                await _roleManager.CreateAsync(new Role("Admin"));
                await _roleManager.CreateAsync(new Role("User"));

                // Create users
                var adminUser = new User
                {
                    UserName = "admin@example.com1",
                    Email = "admin@example.com"
                };

                var userUser = new User
                {
                    UserName = "user@example.com",
                    Email = "user@example.com"
                };

                // Add users to the appropriate roles
                await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                await _userManager.AddToRoleAsync(adminUser, "Admin");

                await _userManager.CreateAsync(userUser, "P@ssw0rd");
                await _userManager.AddToRoleAsync(userUser, "User");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the seeding process
                Console.WriteLine(ex.Message);
            }
        }
    }
}
