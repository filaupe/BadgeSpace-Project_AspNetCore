using Domain.Argumentos.Usuario;
using Domain.Interfaces.Repositorios.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Interfaces.Servicos.Usuario;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Utils;

namespace Web.API.Controllers
{
    [Controller]
    [Route("api/[controller]")]
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioRequest request)
        {
            if (_repositorio.Existe(u => u.Email == request.Email && u.Senha == request.Senha))
            {
                request = await _utils.Completar(request, _context);
                return Ok((await _servicoAutenticacao.GenerateToken(request)).ToString());
            }
            return BadRequest(new { message = "Usuário não existe" });
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UsuarioRequest request)
        {
            await _servicoUsuario.Adicionar(request);
            await _context.SaveChangesAsync();
            return Ok((await _servicoAutenticacao.GenerateToken(request)).ToString());
        }
    }
}
