using System;
using System.ComponentModel.DataAnnotations;

namespace HomeApp.API.Dtos
{
    public class HomeForCreationDto
    {
        public HomeForCreationDto()
        {
            DateAdded = DateTime.Now;

            Random random = new Random();
            int rNum = random.Next(0, 3);
            
            if (rNum == 0) 
            {
                SchoolRating = "A";
            } else if (rNum == 1)
            {
                SchoolRating = "B";
            } else {
                SchoolRating = "C";
            }
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
        [Required]
        public int Bedrooms { get; set; } 
        [Required]
        public int Bathrooms { get; set; }
        [Required]
        public int SquareFeet { get; set; }
        [Required]
        public bool Renevated { get; set; }
        public string SchoolRating { get; set; }
        public DateTime DateAdded { get; set; }
    }
}