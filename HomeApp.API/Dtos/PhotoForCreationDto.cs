using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HomeApp.API.Dtos
{
    public class PhotoForCreationDto
    {
        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
        [Required]
        public IFormFile File { get; set;}
        public string Url { get; set; }
        public string PublicId { get; set; }  // from cloudinary
        public DateTime DateAdded { get; set; }
    }
}