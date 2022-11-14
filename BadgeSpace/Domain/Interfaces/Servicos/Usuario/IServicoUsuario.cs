using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Base;

namespace Domain.Interfaces.Servicos.Usuario
{
    public interface IServicoUsuario
    {
        Task<UsuarioResponse> Adicionar(UsuarioRequest request);
        UsuarioResponse Alterar(UsuarioRequest request);
        UsuarioResponse Selecionar(int id);
        bool VerificarEmail(string Email);
        bool VerificarCPFouCNPJ(string CPFouCNPJ);
        bool VerificarSenha(string Senha);
        IEnumerable<UsuarioResponse> Listar();
        IEnumerable<UsuarioResponse> ListarAtivos();
    }
}
