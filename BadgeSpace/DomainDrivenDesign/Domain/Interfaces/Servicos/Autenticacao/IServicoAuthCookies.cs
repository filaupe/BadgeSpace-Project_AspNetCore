using Domain_Driven_Design.Domain.Argumentos.Usuario;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Base;
using Microsoft.AspNetCore.Http;

namespace Domain_Driven_Design.Domain.Interfaces.Servicos.Autenticacao
{
    public interface IServicoAuthCookies
    {
        Task<bool> GenerateCookies(UsuarioRequest request, HttpContext context);
    }
}
