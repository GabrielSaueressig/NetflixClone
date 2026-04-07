using NetflixClone.Data;
using NetflixClone.Models;
using Microsoft.EntityFrameworkCore;
using DTOs;

namespace Services
{
    public class GenreService
    {
        private readonly AppDbContext _context;

        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        private GenreReadDto MapToReadDto(Genre Genre)
        {
            return new GenreReadDto
            {
                Id = Genre.Id,
                Name = Genre.Name,
            };
        }

        public async Task<List<GenreReadDto>> GetAllAsync()
        {
            return await _context.Genres.Select(g => new GenreReadDto
            {
                Id = g.Id,
                Name = g.Name,

            }).ToListAsync();
        }

        public async Task<GenreReadDto> CreateAsync(GenreCreateDto dto)
        {

            var Genre = new Genre
            {
                Name = dto.Name
            };

            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            return MapToReadDto(Genre);
        }

        public async Task<GenreReadDto?> UpdateAsync(int id, GenreUpdateDto dto)
        {
            var Genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (Genre == null) return null;

            if (dto.Name != null)
                Genre.Name = dto.Name;

            await _context.SaveChangesAsync();

            return MapToReadDto(Genre);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Genre = await _context.Genres.FindAsync(id);
            if (Genre == null) return false;
            _context.Genres.Remove(Genre);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<GenreReadDto?> GetByIdAsync(int id)
        {
            var Genre = await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);

            if (Genre == null) return null;
            
            return MapToReadDto(Genre);
        }
    }
}