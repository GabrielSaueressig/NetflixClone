
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe ao menos um ID de gênero")]
        public List<int> GenreIds { get; set; } = new();
        public string VideoUrl { get; set; } = string.Empty;

        public string PosterUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}