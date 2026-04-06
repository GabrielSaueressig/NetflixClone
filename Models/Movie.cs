namespace NetflixClone.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        public string PosterUrl{get;set;} = string.Empty;
        public string ThumbnailUrl{get;set;} = string.Empty;
        
        public List<Genre> Genres { get; set; } = new();
        public List<User> UsersWhoFavorited { get; set; } = new();
    }
}