using System;
using System.Collections.Generic;
using System.Linq;
using HomeApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HomeApp.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly AppDbContext _context;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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
                    Created = DateTime.Now,
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",  //REQUIRED
                };

                IdentityResult result = _userManager.CreateAsync(user, "password123").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByEmailAsync("admin@admin.com".Normalize()).Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" }).Wait();
                } 
            }

            if (!_context.Homes.Any())
            {
                var homeData = System.IO.File.ReadAllText("Data/HomeSeedData.json");
                var homes = JsonConvert.DeserializeObject<List<Home>>(homeData);

            _context.Homes.AddRange(homes);  //hmmm  //wasnt homes before
            _context.SaveChanges();
            }
    }
    
  }
}