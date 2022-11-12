using AutoMapper;
using Domain.Argumentos.Estudante;
using Domain.Argumentos.Usuario;
using Domain.Interfaces.Repositorios.Estudante;
using Domain.Interfaces.Servicos.Estudante;
using Domain.Recurso.Enums;
using Infra;
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

        private Task<Domain.Entidades.Usuario.Usuario> GetUsuarioAsync { get => _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == User.Claims.ToList()[2].Value)!;}

        public EstudantesController(ApplicationDbContext context, IServicoEstudante servicoEstudante,
            IRepositorioEstudante repositorio, ControllerUtils utils, IMapper mapper)
        {
            _context = context;
            _servicoEstudante = servicoEstudante;
            _repositorio = repositorio;
            _utils = utils;
            _mapper = mapper;
        } 

        public async Task<IActionResult> Index(int skip = 0, int take = 8)
        {
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await _context.Estudantes.CountAsync()*1M / take));
            return View(_servicoEstudante.Listar(skip, take));
        }

        public IActionResult Delete(int id) => View(_servicoEstudante.Selecionar(id));

        public IActionResult Details(int id) => View(_servicoEstudante.Selecionar(id));

        public async Task<IActionResult> Edit(int id) => View(_mapper.Map<EstudanteRequest>(await _context.Estudantes.FindAsync(id)));

        public IActionResult Create() => View();


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnCreate(IFormFile Imagem, EstudanteRequest request)
        {
            request.Empresa = await GetUsuarioAsync;

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                request.Imagem = memoryStream.ToArray();
            }

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
