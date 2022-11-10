using AutoMapper;
using Domain.Argumentos.Estudante;
using Domain.Interfaces.Repositorios.Estudante;
using Domain.Interfaces.Servicos.Estudante;

namespace Infra.Servicos.Estudante
{
    public class ServicoEstudante : IServicoEstudante
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositorioEstudante _repositorio;
        private readonly IMapper _mapper;

        public ServicoEstudante(IRepositorioEstudante repositorio, IMapper mapper, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _context = context;
        }

        public async Task<EstudanteResponse> Adicionar(EstudanteRequest request)
        {
            if (request == null)
                return null!;

            var entidade = new Domain.Entidades.Estudante.Estudante(request);

            return _mapper.Map<EstudanteResponse>(_repositorio.Adicionar(entidade));
        }

        public EstudanteResponse Alterar(EstudanteRequest request)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstudanteResponse> Listar() => _mapper.Map<List<EstudanteResponse>>(_context.Estudantes).ToList();

        public IEnumerable<EstudanteResponse> ListarAtivos() => _mapper.Map<List<EstudanteResponse>>(_context.Estudantes).ToList();

        public EstudanteResponse Selecioanr(int id)
        {
            if (id == 0)
                return null!;
            return _mapper.Map<EstudanteResponse>(_repositorio.OrdenarPorId(id));
        }
    }
}
