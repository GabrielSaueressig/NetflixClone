
using System.ComponentModel.DataAnnotations;

namespace NetflixClone.DTOs
{
    public class GenreCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}