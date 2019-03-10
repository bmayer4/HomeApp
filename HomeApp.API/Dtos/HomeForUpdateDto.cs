using System.ComponentModel.DataAnnotations;

namespace HomeApp.API.Dtos
{
    public class HomeForUpdateDto
    {
        [Required]  //required because they would save into db null if empty
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Price { get; set; }
        public int Bedrooms { get; set; }  
        public int Bathrooms { get; set; }
        public int SquareFeet { get; set; }
        public bool Renevated { get; set; }
        public string SchoolRating { get; set; } 
    }  
}