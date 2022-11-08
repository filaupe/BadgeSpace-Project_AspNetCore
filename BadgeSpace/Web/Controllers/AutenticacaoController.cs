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
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ActionName("Logar")]
        public async Task<IActionResult> Logar(UsuarioRequest request)
        { 
            if (_repositorio.Existe(u => u.Email == request.Email && u.Senha == request.Senha))
            {
                request = await _utils.Completar(request, _context);
                await _servicoAutenticacao.GenerateCookies(request, HttpContext);
                return RedirectToAction("Index", "Home");
            }
            return Redirect(nameof(Login));
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Registrar(UsuarioRequest request)
        {
            await _servicoUsuario.Adicionar(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [ActionName("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
