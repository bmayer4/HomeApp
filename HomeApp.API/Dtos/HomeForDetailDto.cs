using System;
using System.Collections.Generic;
using System.Linq;
using HomeApp.API.Models;

namespace HomeApp.API.Dtos
{
    public class HomeForDetailDto
    {
        public int Id { get; set; }
        public string CoverUrl { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFeet { get; set; }
        public bool Renevated { get; set; }
        public string SchoolRating { get; set; }
        public int DaysOnMarket { get; set; }

        //public int NumberOfPhotos { get { return Photos.Count(); }}
        public int NumberOfPhotos => Photos.Count;
        public ICollection<PhotoForDetailDto> Photos { get; set; } = new List<PhotoForDetailDto>();
        public int UserId { get; set; }
        public string UserEmail { get; set; }
    }
}