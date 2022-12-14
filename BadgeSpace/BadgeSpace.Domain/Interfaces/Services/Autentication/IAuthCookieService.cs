using BadgeSpace.Domain.Entities.User;
using Microsoft.AspNetCore.Http;

namespace BadgeSpace.Domain.Interfaces.Services.Autentication
{
    public interface IAuthCookieService
    {
        Task GenerateCookies(UserModel user, HttpContext context);
    }
}
