using NetflixClone.Data;
using NetflixClone.Models;
using System.Linq;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace NetflixClone.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            return await _context.Users.Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToListAsync();
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

            if(dto.Name != null)
                user.Name = dto.Name;
            if(dto.Email != null)
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
            var user =await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
            
        }
        public async Task<UserReadDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}