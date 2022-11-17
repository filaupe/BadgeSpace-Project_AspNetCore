using Domain_Driven_Design.Domain.Interfaces.Repositorios.Usuario;
using Domain_Driven_Design.Infra.Repositorios.Base;

namespace Domain_Driven_Design.Infra.Repositorios.Usuario
{
    public class RepositorioUsuario : RepositorioBase<Domain_Driven_Design.Domain.Entidades.Usuario.Usuario, int>, IRepositorioUsuario
    {
        private readonly ApplicationDbContext _context;

        public RepositorioUsuario(ApplicationDbContext context) : base(context) => _context = context;
    }
}
