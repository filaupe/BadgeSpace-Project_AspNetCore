using AutoMapper;
using Domain.Argumentos.Usuario;
using Domain.Argumentos.Usuario.Requests;
using Domain.Interfaces.Repositorios.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Interfaces.Servicos.Usuario;
using Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Utils;

namespace Web.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IServicoAuthCookies _servicoAutenticacao;
        private readonly ApplicationDbContext _context;
        private readonly IRepositorioUsuario _repositorio;
        private readonly ControllerUtils _utils;
        private readonly IMapper _mapper;

        public AutenticacaoController(IServicoUsuario servicoUsuario, ApplicationDbContext context,
            IRepositorioUsuario repositorio, IServicoAuthCookies authCookies, ControllerUtils utils,
            IMapper mapper)
        {
            _servicoUsuario = servicoUsuario;
            _context = context;
            _repositorio = repositorio;
            _servicoAutenticacao = authCookies;
            _utils = utils;
            _mapper = mapper;
        }

        public IActionResult Login() 
            => User.Identity!.IsAuthenticated 
                ? RedirectToAction("Index", "Home") : View();

        public IActionResult Register() 
            => User.Identity!.IsAuthenticated 
                ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(UsuarioLogin request)
        {
            var usuario = new UsuarioRequest() { Email = request.Email, Senha = request.Senha };
            if (_repositorio.Existe(u => u.NormalizedEmail == request.Email.ToUpper() && u.Senha == request.Senha))
            {
                usuario = await _utils.Completar(usuario, _context);
                await _servicoAutenticacao.GenerateCookies(usuario, HttpContext);
                return RedirectToAction("Index", "Home");
            }
            return View(request);
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(UsuarioRequest request)
        {
            if (ModelState.IsValid)
            {
                await _servicoUsuario.Adicionar(request);
                await _context.SaveChangesAsync();
                await Login(new UsuarioLogin() { Email = request.Email, Senha = request.Senha });
                return RedirectToAction("Index", "Home");
            }
            return View(request);
        }

        [ActionName("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity!.IsAuthenticated)
                await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
