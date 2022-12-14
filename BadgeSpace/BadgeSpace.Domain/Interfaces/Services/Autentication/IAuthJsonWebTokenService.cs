using BadgeSpace.Domain.Entities.User;

namespace BadgeSpace.Domain.Interfaces.Services.Autentication
{
    public interface IAuthJsonWebTokenService
    {
        Task<string> GenerateToken(UserModel user);
    }
}
