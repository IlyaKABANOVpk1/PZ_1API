using PZ_1API.Models;
using PZ_1API.Models.DTO;

namespace PZ_1API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        string GenerateJwtToken(User user);
    }
}
