using BadgeSpace.Models;

namespace BadgeSpace.Services.Interfaces
{
    public interface IApiAuthService
    {
        Task<string> GenerateToken(ApplicationUser appUser);
    }
}
