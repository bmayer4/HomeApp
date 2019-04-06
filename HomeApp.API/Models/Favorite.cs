namespace HomeApp.API.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int HomeId { get; set; }
        public Home Home { get; set; }
    }
}