using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<DbInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying migrations");
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
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        _logger.LogError("Failed to create role '{Role}'", role);
                    }
                }
            }

            // Create default admin user if not exists
            var adminEmail = "admin@admin.admin";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true 
                };

                var password = "Admin123";
                var result = await _userManager.CreateAsync(adminUser, password);
                if (result.Succeeded)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, DenscuraRoles.Denscura_Admin);
                    if (!addToRoleResult.Succeeded)
                    {
                        _logger.LogError("Failed to add user '{Email}' to role '{Role}'", adminEmail, DenscuraRoles.Denscura_Admin);
                    }
                }
                else
                {
                    _logger.LogError("Failed to create user '{Email}'", adminEmail);
                }
            }
        }
    }
}
