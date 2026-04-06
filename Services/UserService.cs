using NetflixClone.Data;
using NetflixClone.Models;
using System.Linq;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Models;

namespace NetflixClone.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        private UserReadDto MapToReadDto(User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,

                Favorites = user.Favorites.Select(f => new MovieReadDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    Description = f.Description,
                    VideoUrl = f.VideoUrl,
                    PosterUrl = f.PosterUrl,
                    ThumbnailUrl = f.ThumbnailUrl,

                    Genres = f.Genres.Select(g => new GenreReadDto
                    {
                        Id = g.Id,
                        Name = g.Name

                    }).ToList()
                }).ToList()
            };
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var users = await _context.Users.Include(u => u.Favorites).ThenInclude(f => f.Genres).ToListAsync();
            return users.Select(u => MapToReadDto(u)).ToList();
        }

        public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserReadDto?> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (dto.Name != null)
                user.Name = dto.Name;
            if (dto.Email != null)
                user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<UserReadDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Favorites).ThenInclude(f => f.Genres).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;
            return MapToReadDto(user);
        }

        public async Task<bool> AddFavoriteAsync(int id, int movieId)
        {
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == id);
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if(user == null || movie == null)
                return false;

            if(user.Favorites.Any(m => m.Id == movieId))
                return false;
            user.Favorites.Add(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavoriteAsync(int id, int movieId)
        {
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == id);
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if(user == null || movie == null)
                return false;

            if(!user.Favorites.Any(m => m.Id == movieId))
                return false;
                
            user.Favorites.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}