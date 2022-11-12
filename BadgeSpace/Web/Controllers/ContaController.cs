using Domain.Argumentos.Usuario;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Interfaces.Servicos.Usuario;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers.Utils;

namespace Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IServicoAuthJWT _authJWT;
        private readonly ControllerUtils _utils;

        private Domain.Entidades.Usuario.Usuario UserCookie { get => Task.Run(async () => await _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == User.Claims.ToList()[2].Value)).Result!; }

        public ContaController(ApplicationDbContext context, IServicoAuthJWT authJWT, IServicoUsuario servicoUsuario,
            ControllerUtils utils)
        {
            _context = context;
            _authJWT = authJWT;
            _servicoUsuario = servicoUsuario;
            _utils = utils;
        }

        public IActionResult Index() => View(UserCookie);

        public IActionResult ChangePassword() => View(UserCookie);

        public IActionResult Email() => View(UserCookie);

        public IActionResult ApiKey() => View(UserCookie);

        public async Task<IActionResult> ChangeToken()
        {
            var token = (await _authJWT.GenerateToken(UserCookie.Id, UserCookie.Claim, UserCookie.Email)).ToString();
            UserCookie.AtualizarToken(token);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ApiKey)); 
        }
    }
}
