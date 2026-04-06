using DTOs;

namespace Models
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<GenreReadDto> Genres { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;

    }
}