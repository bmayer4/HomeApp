using System;

namespace HomeApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsCover { get; set; }
        public string PublicId { get; set; }
        public Home Home { get; set; }
        public int HomeId { get; set; }
    }
}