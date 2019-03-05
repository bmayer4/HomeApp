using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HomeApp.API.Models
{
    public class User: IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}