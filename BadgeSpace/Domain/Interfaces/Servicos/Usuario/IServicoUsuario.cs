using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Base;

namespace Domain.Interfaces.Servicos.Usuario
{
    public interface IServicoUsuario
    {
        Task<UsuarioResponse> Adicionar(UsuarioRequest request);
        UsuarioResponse Alterar(UsuarioRequest request);
        UsuarioResponse Selecioanr(int id);
        IEnumerable<UsuarioResponse> Listar();
        IEnumerable<UsuarioResponse> ListarAtivos();
    }
}
