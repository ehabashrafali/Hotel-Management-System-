using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hotel_Booking.Repo.Services
{
    public class AdminInitializer : IAdminInitializer
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public AdminInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
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
                Console.WriteLine(ex.Message);
            }

            if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
            {
                var adminRole = new IdentityRole(Roles.Admin);
                var customerRole = new IdentityRole(Roles.Customer);

                _roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                _roleManager.CreateAsync(customerRole).GetAwaiter().GetResult();

                var user = new AppUser
                {
                    FullName = "EhabAshraf",
                    Email = "Ehabbbb@gmail.com",
                    UserName = "ehabashraf",
                };

                var result = _userManager.CreateAsync(user, "Ehab@123").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    var createdUser = _userManager.FindByEmailAsync("Ehabbbb@gmail.com").GetAwaiter().GetResult();

                    if (createdUser != null)
                    {
                        _userManager.AddToRoleAsync(createdUser, Roles.Admin).GetAwaiter().GetResult();
                    }
                    else
                    {
                        // Handle the case where the user wasn't found
                    }
                }
                else
                {
                    // Handle the case where user creation failed (e.g., due to validation errors)
                    foreach (var error in result.Errors)
                    {
                        // Handle or log the errors
                    }
                }
            }
        }

    }
}
