using NetflixClone.Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class MovieUpdateDto
    {

        public string? Title { get; set; }

        public List<int>? GenresId { get; set; }

        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? PosterUrl { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; } = string.Empty;
    }
}