using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HomeApp.API.Models
{
    public class User: IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Home> Homes { get; set; } = new List<Home>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}