using Domain_Driven_Design.Domain.Argumentos.Usuario;
using Domain_Driven_Design.Infra;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Utils
{
    public class ControllerUtils
    {
        public async Task<UsuarioRequest> Completar(UsuarioRequest request, ApplicationDbContext context)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.NormalizedEmail == request.Email.ToUpper() && u.Senha == request.Senha);

            request.Nome = usuario!.Nome;
            request.CPFouCNPJ = usuario.CPFouCNPJ;
            request.NormalizedEmail = usuario.Email.ToUpper();
            request.ConfirmarSenha = usuario.Senha;
            request.Imagem = usuario.Imagem;
            request.Claim = usuario.Claim;
            request.Token = usuario.Token;

            return request;
        }
    }
}
