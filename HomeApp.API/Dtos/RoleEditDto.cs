using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeApp.API.Dtos
{
    public class RoleEditDto
    {
        [Required]
        public List<string> RoleNames { get; set; }
    }
}