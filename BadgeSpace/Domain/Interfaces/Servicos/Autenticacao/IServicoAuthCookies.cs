using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Servicos.Autenticacao
{
    public interface IServicoAuthCookies
    {
        Task<bool> GenerateCookies(UsuarioRequest request, HttpContext context);
    }
}
