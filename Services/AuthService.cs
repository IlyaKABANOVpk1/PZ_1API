using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PZ_1API.Models;
using PZ_1API.Models.DTO;
using PZ_1API.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PZ_1API.Services
{
    public class AuthService : IAuthService
    {
        private readonly CookbookDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(CookbookDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                throw new Exception("Email уже зарегистрирован.");

            if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
                throw new Exception("Username уже занят.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = 2
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.Roles.FindAsync(user.RoleId);

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                Username = user.Username,
                Role = role?.Name
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.UsernameOrEmail ||
                    x.Username == dto.UsernameOrEmail);

            if (user == null)
                throw new Exception("Пользователь не найден.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Неверный пароль.");

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                Username = user.Username,
                Role = user.Role.Name
            };
        }

        public string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
