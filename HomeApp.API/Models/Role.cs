using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HomeApp.API.Models
{
    public class Role: IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}