
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.utilities
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }


            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, such as logging it
                Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                throw;
            }

            var roles = new[]
            {
                DenscuraRoles.Denscura_Admin,
                DenscuraRoles.Denscura_Patient,
                DenscuraRoles.Denscura_Dentist
            };
            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role).Result)
                {
                    var result = _roleManager.CreateAsync(new IdentityRole(role)).Result;
                    if (!result.Succeeded)
                    {
                        // Handle role creation failure
                        Console.WriteLine($"Failed to create role '{role}'");
                    }
                }
            }

            // Create default admin user if not exists
            var adminEmail = "yalung@admin.com";
            var adminUser = _userManager.FindByEmailAsync(adminEmail).Result;
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "Yalung",
                    Email = adminEmail
                };

                // Generate a secure password
                var password = "Yalung@123"; // Change this to a secure default password
                adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, password);

                var result = _userManager.CreateAsync(adminUser).Result;
                if (result.Succeeded)
                {
                    var addToRoleResult = _userManager.AddToRoleAsync(adminUser, DenscuraRoles.Denscura_Admin).Result;
                    if (!addToRoleResult.Succeeded)
                    {
                        // Handle adding user to role failure
                        Console.WriteLine($"Failed to add user '{adminEmail}' to role '{DenscuraRoles.Denscura_Admin}'");
                    }
                }
                else
                {
                    // Handle user creation failure
                    Console.WriteLine($"Failed to create user '{adminEmail}'");
                }
            }
        }
    }
}
