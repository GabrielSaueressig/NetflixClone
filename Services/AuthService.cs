using DTOs;
using NetflixClone.Data;
using NetflixClone.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.email.ToLower());
            if (user == null)
                return null;

            var correctPassword = BCrypt.Net.BCrypt.Verify(loginDto.password, user.PasswordHash);
            if (!correctPassword)
                return null;

            return GenerateToken(user);
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.Name, user.Email),
              new Claim("id", user.Id.ToString()),
              new Claim(ClaimTypes.Role, user.Role)
            };

            var secretKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("Chave JWT não configurada!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer:_configuration["Jwt:Issuer"], audience:_configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}   