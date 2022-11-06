using Domain.Interfaces.Repositorios.Estudante;
using Infra.Repositorios.Base;

namespace Infra.Repositorios.Estudante
{
    public class RepositorioEstudante : RepositorioBase<Domain.Entidades.Estudante.Estudante, int>, IRepositorioEstudante
    {
        private readonly ApplicationDbContext _context;

        public RepositorioEstudante(ApplicationDbContext context) : base(context) => _context = context;
    }
}
