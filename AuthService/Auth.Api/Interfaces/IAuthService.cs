using AuthService.Models;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(User user);
        Task<string> SetRefreshToken(User user);
    }
}
