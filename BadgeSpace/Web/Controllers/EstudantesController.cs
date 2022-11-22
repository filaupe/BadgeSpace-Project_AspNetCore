using AutoMapper;
using Domain_Driven_Design.Domain.Argumentos.Estudante;
using Domain_Driven_Design.Domain.Interfaces.Repositorios.Estudante;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Estudante;
using Domain_Driven_Design.Domain.Recursos.Enums;
using Domain_Driven_Design.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers.Utils;

namespace Web.Controllers
{
    [Authorize(Roles = nameof(Roles.EMPRESA))]
    public class EstudantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicoEstudante _servicoEstudante;
        private readonly IRepositorioEstudante _repositorio;
        private readonly IMapper _mapper;
        private readonly ControllerUtils _utils;

        private Task<Domain_Driven_Design.Domain.Entidades.Usuario.Usuario?> GetUsuarioAsync { get => User.Identity!.IsAuthenticated ? _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == User.Claims.ToList()[2].Value)! : null!;}

        public EstudantesController(ApplicationDbContext context, IServicoEstudante servicoEstudante,
            IRepositorioEstudante repositorio, ControllerUtils utils, IMapper mapper)
        {
            _context = context;
            _servicoEstudante = servicoEstudante;
            _repositorio = repositorio;
            _utils = utils;
            _mapper = mapper;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Index(string searchString = "", int skip = 0, int take = 8)
        {
            var listaPorUsuario = _context.Estudantes.Where(s => s.Empresa!.CPFouCNPJ == User.Claims.ToList()[2].Value);

            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await listaPorUsuario.CountAsync()*1M / take));

            if (!String.IsNullOrEmpty(searchString))
                listaPorUsuario = listaPorUsuario.Where(s => s.CPF!.Contains(searchString));

            return View(await listaPorUsuario.AsNoTracking().Skip(skip * take).Take(take).ToListAsync());
        }

        public async Task<IActionResult> Delete(int id)
            => (await _context.Estudantes.FindAsync(id))!.EmpresaId == (await GetUsuarioAsync)!.Id
                ? View(_servicoEstudante.Selecionar(id)) : BadRequest("Acesso Negado");

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id) 
        {
            var erroMsg = NotFound($"O Estudante não existe");
            if (int.TryParse(id, out int n))
            {
                var estudantePorId = _servicoEstudante.Selecionar(n);
                if (estudantePorId == null)
                    return erroMsg;
                return View(estudantePorId);
            }
            var estudantePorCodigo = await _context.Estudantes.FirstOrDefaultAsync(e => e.Codigo == id);
            if(estudantePorCodigo == null)
                return erroMsg;
            return View(_servicoEstudante.Selecionar(estudantePorCodigo.Id));
        }

        public async Task<IActionResult> Edit(int id)
            => (await _context.Estudantes.FindAsync(id))!.EmpresaId == (await GetUsuarioAsync)!.Id 
                ? View(_mapper.Map<EstudanteRequest>(await _context.Estudantes.FindAsync(id))) : BadRequest("Acesso Negado");

        public async Task<IActionResult> Create() 
            => View(_mapper.Map<EstudanteRequest>(await _context.Estudantes.OrderBy(s => s.Empresa!.CPFouCNPJ == User.Claims.ToList()[2].Value).LastOrDefaultAsync()));


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnCreate(IFormFile? Imagem, EstudanteRequest request)
        {
            var lastUser = await _context.Estudantes.OrderBy(i => i.Id).LastOrDefaultAsync(s => s.Empresa!.CPFouCNPJ == User.Claims.ToList()[2].Value);
            request.Empresa = await GetUsuarioAsync;

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                request.Imagem = memoryStream.ToArray();
            }

            if(lastUser != null)
                request.Imagem = Imagem == null ? lastUser.Imagem : request.Imagem;

            if (Imagem == null)
                return View(request);

            if (ModelState.IsValid)
            {
                await _servicoEstudante.Adicionar(request);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnEdit(int id, IFormFile Imagem, EstudanteRequest request)
        {
            request.Empresa = await GetUsuarioAsync;

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                request.Imagem = memoryStream.ToArray();
            }

            _servicoEstudante.Alterar(_repositorio.OrdenarPorId(id), request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnDelete(int id)
        {
            _repositorio.Remover((await _context.Estudantes.FindAsync(id))!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
