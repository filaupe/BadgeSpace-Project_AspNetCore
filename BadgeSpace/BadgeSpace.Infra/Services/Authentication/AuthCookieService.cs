using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
using BadgeSpace.Domain.Resources.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BadgeSpace.Infra.Services.Authentication
{
    public class AuthCookieService : IAuthCookieService
    {
        public async Task GenerateCookies(UserModel user, HttpContext context)
            => await Task.Run(async () =>
            {
                List<Claim> direitosAcesso = new()
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.NormalizedEmail),
                    new Claim(ClaimTypes.Role, user.Claim ? nameof(Roles.EMPRESA) : nameof(Roles.ESTUDANTE))
                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await context.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(15),
                    });
            });
    }
}
