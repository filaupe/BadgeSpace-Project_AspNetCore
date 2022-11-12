using AutoMapper;
using Domain.Argumentos.Usuario;
using Domain.Interfaces.Repositorios.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Interfaces.Servicos.Usuario;

namespace Infra.Servicos.Usuario
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorio;
        private readonly IMapper _mapper;
        private readonly IServicoAuthJWT _authJWT;

        public ServicoUsuario(IRepositorioUsuario repositorio, IMapper mapper, IServicoAuthJWT authJWT)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _authJWT = authJWT;
        }

        public async Task<UsuarioResponse> Adicionar(UsuarioRequest request)
        {
            request.Nome ??= request.Email![0..request.Email!.IndexOf("@")];
            request.NormalizedEmail = request.Email.ToUpper();
            request.Token = (await _authJWT.GenerateToken(request.Id, request.Claim, request.Email)).ToString();

            var entidade = new Domain.Entidades.Usuario.Usuario(request);

            return _mapper.Map<UsuarioResponse>(_repositorio.Adicionar(entidade));
        }

        public UsuarioResponse Alterar(UsuarioRequest request)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioResponse> Listar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioResponse> ListarAtivos()
        {
            throw new NotImplementedException();
        }

        public UsuarioResponse Selecioanr(int id)
        {
            if(id == 0)
                return null!;
            return _mapper.Map<UsuarioResponse>(_repositorio.OrdenarPorId(id));
        }
    }
}
