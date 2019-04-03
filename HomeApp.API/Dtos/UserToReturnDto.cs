using System;

namespace HomeApp.API.Dtos
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
    }
}