using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Recurso.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Servicos.Autenticacao
{
    public class AuthCookies : IServicoAuthCookies
    {
        public async Task<bool> GenerateCookies(UsuarioRequest request, HttpContext context)
        => await Task.Run(async () =>
        {
            List<Claim> direitosAcesso = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, request!.Id.ToString()),
                    new Claim(ClaimTypes.Name, request.Nome),
                    new Claim(ClaimTypes.Role, request.Claim ? nameof(Roles.Empresa) : nameof(Roles.Usuario))
                };

            var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
            var userPrincipal = new ClaimsPrincipal(new[] { identity });

            await context.SignInAsync(userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30),
                });
            return true;
        });
    }
}
