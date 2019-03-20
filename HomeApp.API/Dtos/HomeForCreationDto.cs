using System;
using System.ComponentModel.DataAnnotations;

namespace HomeApp.API.Dtos
{
    public class HomeForCreationDto
    {
        public HomeForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int Bedrooms { get; set; }  // could be land
        public int Bathrooms { get; set; }
        public int SquareFeet { get; set; }
        public bool Renevated { get; set; }
        public string SchoolRating { get; set; }
        public DateTime DateAdded { get; set; }
    }
}