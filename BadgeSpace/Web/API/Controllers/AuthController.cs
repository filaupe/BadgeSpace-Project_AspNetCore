using Domain.Argumentos.Usuario;
using Domain.Argumentos.Usuario.Requests;
using Domain.Interfaces.Repositorios.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Interfaces.Servicos.Usuario;
using Infra;
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
            return BadRequest(new { message = "Usuário não existe" });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Register([FromBody] UsuarioRequest request)
        {
            var response = new { request.Nome, request.Email, request.CPFouCNPJ, request.Senha, request.ConfirmarSenha };
            if (_repositorio.Existe(u => u.Email == request.Email))
            {
                request.Email = "Ja existe uma conta com esse Email";
                return BadRequest(response);
            }
            if (_repositorio.Existe(u => u.CPFouCNPJ == request.CPFouCNPJ))
            {
                request.CPFouCNPJ = "Ja existe uma conta com esse CPF ou CNPJ";
                return BadRequest(response);
            }
            if (ModelState.IsValid)
            {
                await _servicoUsuario.Adicionar(request);
                await _context.SaveChangesAsync();
                return Ok((await _servicoAutenticacao.GenerateToken(request.Id, request.Claim, request.Email, request.CPFouCNPJ)).ToString());
            }
            return BadRequest(response);
        }
    }
}
