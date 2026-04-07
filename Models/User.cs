using Models;

namespace NetflixClone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role{get;set;} = "Client";
        public List<Movie> Favorites{get;set;} = new();
    }
}