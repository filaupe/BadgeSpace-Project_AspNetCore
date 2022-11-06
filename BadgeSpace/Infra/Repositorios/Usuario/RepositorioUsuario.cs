using Domain.Interfaces.Repositorios.Usuario;
using Infra.Repositorios.Base;

namespace Infra.Repositorios.Usuario
{
    public class RepositorioUsuario : RepositorioBase<Domain.Entidades.Usuario.Usuario, int>, IRepositorioUsuario
    {
        private readonly ApplicationDbContext _context;

        public RepositorioUsuario(ApplicationDbContext context) : base(context) => _context = context;
    }
}
