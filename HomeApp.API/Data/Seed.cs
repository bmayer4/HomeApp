using System.Collections.Generic;
using System.Linq;
using HomeApp.API.Models;
using Microsoft.AspNetCore.Identity;

namespace HomeApp.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers() 
        {
            if (!_userManager.Users.Any()) {  
                var roles = new List<Role> {
                    new Role() { Name = "Member" },
                    new Role() { Name = "Admin" },
                };

                foreach (var role in roles) {
                    _roleManager.CreateAsync(role).Wait();
                }

                var user = new User()
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(user, "password123").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" }).Wait();
                }
            }
    }
    
  }
}