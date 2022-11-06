using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Base;

namespace Domain.Interfaces.Servicos.Autenticacao
{
    public interface IServicoAuthJWT
    {
        Task<string> GenerateToken(UsuarioRequest request);
    }
}
