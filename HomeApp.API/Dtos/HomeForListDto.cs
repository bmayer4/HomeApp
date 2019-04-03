using System;
using System.Collections.Generic;
using System.Linq;
using HomeApp.API.Models;

namespace HomeApp.API.Dtos
{
    public class HomeForListDto
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int DaysOnMarket { get; set; }
        public int SquareFeet { get; set; }
        public string CoverUrl { get; set; }
        public int NumberOfPhotos { get; set; }
    }
}