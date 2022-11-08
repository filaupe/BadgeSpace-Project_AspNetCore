using Domain.Argumentos.Usuario;
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

        public AutenticacaoController(IServicoUsuario servicoUsuario, ApplicationDbContext context,
            IRepositorioUsuario repositorio, IServicoAuthCookies authCookies, ControllerUtils utils)
        {
            _servicoUsuario = servicoUsuario;
            _context = context;
            _repositorio = repositorio;
            _servicoAutenticacao = authCookies;
            _utils = utils;
        }

        public IActionResult Login() 
            => User.Identity!.IsAuthenticated 
                ? RedirectToAction("Index", "Home") : View();

        public IActionResult Register() 
            => User.Identity!.IsAuthenticated 
                ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(UsuarioRequest request)
        { 
            if (_repositorio.Existe(u => u.Email == request.Email && u.Senha == request.Senha))
            {
                request = await _utils.Completar(request, _context);
                await _servicoAutenticacao.GenerateCookies(request, HttpContext);
                return RedirectToAction("Index", "Home");
            }
            if (!_repositorio.Existe(u => u.Email == request.Email))
                ViewBag.errorMessageEmail = "Email incorreto";
            if (!_repositorio.Existe(u => u.Senha == request.Senha))
                ViewBag.errorMessageSenha = "Senha incorreta";
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
                await Login(request);
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
