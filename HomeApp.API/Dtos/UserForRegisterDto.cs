using System;
using System.ComponentModel.DataAnnotations;

namespace HomeApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 14 characters.")]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }
        
        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}