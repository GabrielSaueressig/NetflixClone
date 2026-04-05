
using System.ComponentModel.DataAnnotations;

namespace NetflixClone.DTOs
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe ao menos um ID de gênero")]
        public List<int> GenreIds { get; set; } = new();

        [Required(ErrorMessage = "A Url do vídeo é obrigatoria")]
        public string VideoUrl{get;set;} = string.Empty;
    }
}