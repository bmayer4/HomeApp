using System;

namespace HomeApp.API.Dtos
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsProfessional { get; set; }
    }
}