using AutoMapper;
using Domain.Argumentos.Estudante;
using Domain.Interfaces.Repositorios.Estudante;
using Domain.Interfaces.Servicos.Estudante;

namespace Domain.Servicos.Estudante
{
    public class ServicoEstudante : IServicoEstudante
    {
        private readonly IRepositorioEstudante _repositorio;
        private readonly IMapper _mapper;

        public ServicoEstudante(IRepositorioEstudante repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<EstudanteResponse> Adicionar(EstudanteRequest request)
        {
            if (request == null)
                return null!;

            var entidade = new Entidades.Estudante.Estudante(request);

            // if (ModelState.IsValid) return null!;

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

        public IEnumerable<EstudanteResponse> Listar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstudanteResponse> ListarAtivos()
        {
            throw new NotImplementedException();
        }

        public EstudanteResponse Selecioanr(int id)
        {
            if (id == 0)
                return null!;
            return _mapper.Map<EstudanteResponse>(_repositorio.OrdenarPorId(id));
        }
    }
}
