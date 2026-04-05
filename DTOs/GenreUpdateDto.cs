using NetflixClone.Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class GenreUpdateDto
    {

        public string? Name { get; set; }

        public List<int>? MoviesId { get; set; }

    }
}