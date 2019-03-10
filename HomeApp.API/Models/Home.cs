using System;
using System.Collections.Generic;

namespace HomeApp.API.Models
{
    public class Home
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFeet { get; set; }
        public bool Renevated { get; set; }
        public string SchoolRating { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public User User { get; set; }
        public int UserId { get; set; }
    }
}