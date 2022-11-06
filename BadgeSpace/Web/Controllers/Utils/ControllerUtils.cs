using Domain.Argumentos.Usuario;
using Infra;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Utils
{
    public class ControllerUtils
    {
        public async Task<UsuarioRequest> Completar(UsuarioRequest request, ApplicationDbContext context)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email && u.Senha == request.Senha);

            request.Nome = usuario!.Nome;
            request.CPFouCNPJ = usuario.CPFouCNPJ;
            request.Imagem = usuario.Imagem;
            request.Claim = usuario.Claim;
            request.Token = usuario.Token;

            return request;
        }
    }
}
