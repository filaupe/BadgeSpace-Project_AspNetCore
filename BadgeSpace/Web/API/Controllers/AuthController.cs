using Domain_Driven_Design.Domain.Argumentos.Usuario;
using Domain_Driven_Design.Domain.Argumentos.Usuario.Requests;
using Domain_Driven_Design.Domain.Interfaces.Repositorios.Usuario;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Autenticacao;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Usuario;
using Domain_Driven_Design.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Utils;

namespace Web.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/autenticacao")]
    public class AuthController : ControllerBase
    {
        private readonly IServicoAuthJWT _servicoAutenticacao;
        private readonly IServicoUsuario _servicoUsuario;
        private readonly ApplicationDbContext _context;
        private readonly IRepositorioUsuario _repositorio;
        private readonly ControllerUtils _utils;

        public AuthController(IServicoAuthJWT servicoAutenticacao, ApplicationDbContext context,
            ControllerUtils utils, IRepositorioUsuario repositorio, IServicoUsuario servicoUsuario)
        {
            _servicoAutenticacao = servicoAutenticacao;
            _context = context;
            _utils = utils;
            _repositorio = repositorio;
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin request)
        {
            var usuario = new UsuarioRequest() { Email = request.Email, Senha = request.Senha };
            if (_repositorio.Existe(u => u.NormalizedEmail == request.Email.ToUpper() && u.Senha == request.Senha))
            {
                usuario = await _utils.Completar(usuario, _context);
                return Ok((await _servicoAutenticacao.GenerateToken(usuario.Id, usuario.Claim, usuario.Email, usuario.CPFouCNPJ)).ToString());
            }
            return BadRequest("Credênciais incorretas");
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Register([FromBody] UsuarioRequest request)
        {
            if (ModelState.IsValid)
            {
                  await _servicoUsuario.Adicionar(request);
                await _context.SaveChangesAsync();
                return Ok((await _servicoAutenticacao.GenerateToken(request.Id, request.Claim, request.Email, request.CPFouCNPJ)).ToString());
            }
            return BadRequest(ModelState);
        }
    }
}
