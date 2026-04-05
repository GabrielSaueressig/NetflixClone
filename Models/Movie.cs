using NetflixClone.Models;

namespace Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Genre> Genres { get; set; } = new();
        
    }
}