using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Recurso.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infra.Servicos.Autenticacao
{
    public class AuthCookies : IServicoAuthCookies
    {
        public async Task<bool> GenerateCookies(UsuarioRequest request, HttpContext context)
        => await Task.Run(async () =>
        {
            List<Claim> direitosAcesso = new List<Claim>()
                {
                    new Claim(ClaimTypes.Sid, request!.Id.ToString()),
                    new Claim(ClaimTypes.Name, request.Nome!),
                    new Claim(ClaimTypes.NameIdentifier, request.CPFouCNPJ!),
                    new Claim(ClaimTypes.UserData, request.Imagem != null ? Convert.ToBase64String(request.Imagem) : ""),
                    new Claim(ClaimTypes.Role, request.Claim ? nameof(Roles.EMPRESA) : nameof(Roles.USUARIO))
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
