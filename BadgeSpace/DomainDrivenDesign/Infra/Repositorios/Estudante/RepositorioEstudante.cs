using Domain_Driven_Design.Domain.Interfaces.Repositorios.Estudante;
using Domain_Driven_Design.Infra.Repositorios.Base;

namespace Domain_Driven_Design.Infra.Repositorios.Estudante
{
    public class RepositorioEstudante : RepositorioBase<Domain_Driven_Design.Domain.Entidades.Estudante.Estudante, int>, IRepositorioEstudante
    {
        private readonly ApplicationDbContext _context;

        public RepositorioEstudante(ApplicationDbContext context) : base(context) => _context = context;
    }
}
