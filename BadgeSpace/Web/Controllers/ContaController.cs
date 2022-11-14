using AutoMapper;
using Domain.Argumentos.Usuario;
using Domain.Argumentos.Usuario.Requests;
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
        private readonly IMapper _mapper;
        private readonly ControllerUtils _utils;

        public Domain.Entidades.Usuario.Usuario UserCookie { get => Task.Run(async () => await _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == User.Claims.ToList()[2].Value)).Result!; }

        public ContaController(ApplicationDbContext context, IServicoAuthJWT authJWT, IServicoUsuario servicoUsuario,
            ControllerUtils utils, IMapper mapper)
        {
            _context = context;
            _authJWT = authJWT;
            _servicoUsuario = servicoUsuario;
            _utils = utils;
            _mapper = mapper;
        }

        public IActionResult Index() => View(UserCookie);

        public IActionResult Password() => View();

        public IActionResult Email() => View(_mapper.Map<UsuarioEmail>(UserCookie));

        public IActionResult ApiKey() => View(_mapper.Map<UsuarioToken>(UserCookie));

        public async Task<IActionResult> ChangeToken()
        {
            var token = (await _authJWT.GenerateToken(UserCookie.Id, UserCookie.Claim, UserCookie.Email, UserCookie.CPFouCNPJ)).ToString();
            var usuarioLogin = new UsuarioRequest() { Email = UserCookie.Email, Senha = UserCookie.Senha };
            var usuarioCompleto = _utils.Completar(usuarioLogin, _context).Result;
            usuarioCompleto.Token = token;
            UserCookie.Atualizar(usuarioCompleto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ApiKey)); 
        }

        public async Task<IActionResult> ChangePassword(UsuarioSenha request) 
        {
            var usuarioLogin = new UsuarioRequest() { Email = UserCookie.Email, Senha = UserCookie.Senha };
            var usuarioCompleto = _utils.Completar(usuarioLogin, _context).Result;
            usuarioCompleto.Senha = request.NovaSenha;
            UserCookie.Atualizar(usuarioCompleto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Password));
        }

        public async Task<IActionResult> ChangeEmail(UsuarioEmail request) 
        {
            var usuarioLogin = new UsuarioRequest() { Email = UserCookie.Email, Senha = UserCookie.Senha };
            var usuarioCompleto = _utils.Completar(usuarioLogin, _context).Result;
            usuarioCompleto.Email = request.Email;
            usuarioCompleto.NormalizedEmail = request.Email.ToUpper();
            UserCookie.Atualizar(usuarioCompleto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Email));
        }

        public async Task<IActionResult> ChangeImage(IFormFile Imagem)
        {
            var usuarioLogin = new UsuarioRequest() { Email = UserCookie.Email, Senha = UserCookie.Senha };
            var usuarioCompleto = _utils.Completar(usuarioLogin, _context).Result;
            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                usuarioCompleto.Imagem = memoryStream.ToArray();
            }
            UserCookie.Atualizar(usuarioCompleto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
