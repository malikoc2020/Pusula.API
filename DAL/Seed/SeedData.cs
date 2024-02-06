using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seed
{
    public class SeedData
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PermissionType> _PermissionTypeRepository;

        public SeedData(RoleManager<Role> roleManager, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _PermissionTypeRepository = _unitOfWork.GetRepository<PermissionType>();
        }

        public async Task SeedRolesAndUsersAsync()
        {
            try
            {
                // Check if the database has already been seeded
                if (!(await _roleManager.Roles.AnyAsync()))
                {
                    // Create roles
                    await _roleManager.CreateAsync(new Role("Admin"));
                    await _roleManager.CreateAsync(new Role("User"));
                }
                if (!(await _userManager.Users.AnyAsync()))
                {
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

                if ((await _PermissionTypeRepository.GetAllAsync()).Count==0)
                {
                    var permissonType = new PermissionType() { Name = "Annual Leave" };
                    await _PermissionTypeRepository.AddAsync(permissonType);
                    await _unitOfWork.CommitAsync();
                }

            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the seeding process
                Console.WriteLine(ex.Message);
            }
        }
    }
}
