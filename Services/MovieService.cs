using NetflixClone.Data;
using NetflixClone.Models;
using NetflixClone.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;

namespace NetflixClone.Services
{
    public class MovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        private MovieReadDto MapToReadDto(Movie movie)
        {
            return new MovieReadDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                VideoUrl = movie.VideoUrl,
                Genres = movie.Genres.Select(g => new GenreReadDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
        }

        public async Task<List<MovieReadDto>> GetAllAsync()
        {
            return await _context.Movies.Include(m => m.Genres).Select(m => new MovieReadDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                VideoUrl = m.VideoUrl,

                Genres = m.Genres.Select(g => new GenreReadDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()

            }).ToListAsync();
        }

        public async Task<MovieReadDto> CreateAsync(MovieCreateDto dto)
        {
            var genres = await _context.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToListAsync();

            var Movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Genres = genres,
                VideoUrl = dto.VideoUrl
            };

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return MapToReadDto(Movie);
        }

        public async Task<MovieReadDto?> UpdateAsync(int id, MovieUpdateDto dto)
        {
            var Movie = await _context.Movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

            if (Movie == null) return null;

            if (dto.Title != null)
                Movie.Title = dto.Title;
            if (dto.Description != null)
                Movie.Description = dto.Description;
            if (dto.VideoUrl != null)
                Movie.VideoUrl = dto.VideoUrl;
            if (dto.GenresId != null)
            {
                var newGenres = await _context.Genres.Where(g => dto.GenresId.Contains(g.Id)).ToListAsync();
                Movie.Genres = newGenres;
            }

            await _context.SaveChangesAsync();

            return MapToReadDto(Movie);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            if (Movie == null) return false;
            _context.Movies.Remove(Movie);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<MovieReadDto?> GetByIdAsync(int id)
        {
            var Movie = await _context.Movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

            if (Movie == null) return null;
            
            return MapToReadDto(Movie);
        }
    }
}