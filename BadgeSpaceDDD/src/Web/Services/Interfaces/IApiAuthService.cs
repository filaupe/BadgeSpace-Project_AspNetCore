using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IApiAuthService
    {
        Task<string> GenerateToken(ApplicationUser appUser);
    }
}
