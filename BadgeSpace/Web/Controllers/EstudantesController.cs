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

        public EstudantesController(ApplicationDbContext context, IServicoEstudante servicoEstudante,
            IRepositorioEstudante repositorio, ControllerUtils utils, IMapper mapper)
        {
            _context = context;
            _servicoEstudante = servicoEstudante;
            _repositorio = repositorio;
            _utils = utils;
            _mapper = mapper;
        } 

        public IActionResult Create() => View();

        public async Task<IActionResult> Index(int skip = 0, int take = 25)
        {
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await _context.Estudantes.CountAsync()*1M / take));
            return View(_servicoEstudante.Listar(skip, take));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || !id.HasValue || id.Value == 0)
        //        return NotFound();
        //
        //    var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        //
        //    if (studentModel == null)
        //        return NotFound();
        //
        //    return View(studentModel);
        //}


        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }   
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || !id.HasValue)
        //        return NotFound();
        //
        //    var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        //
        //    if (studentModel == null)
        //        return NotFound();
        //
        //    return View(studentModel);
        //}

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var studentModel = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        //
        //    return View(studentModel);
        //}


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Imagem, EstudanteRequest request)
        {
            var claim = User.Claims.ToList();
            request.Empresa = await _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == claim[2].Value);

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
        public async Task<IActionResult> Edit()
        {
            return View();
        }
        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IFormFile Imagem, StudentModel studentModel)
        //{
        //    if (id != studentModel.Id) return NotFound();
        //
        //    var ok = await CheckIfUserIsValid.IsUserValid(_context.Users, studentModel.AlunoCPF);
        //    if (!ok || !ModelState.IsValid)
        //    {
        //        ViewBag.AuthCPF = "Este CPF não existe";
        //        return View(studentModel);
        //    }
        //
        //    if (Imagem != null)
        //    {
        //        using var memoryStream = new MemoryStream();
        //        await Imagem.CopyToAsync(memoryStream);
        //        studentModel.Imagem = memoryStream.ToArray();
        //    }
        //
        //    var old = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentModel.Id);
        //
        //    if (old == null) return BadRequest();
        //
        //    _context.Update(UserCaseExtension.OldToNewRegister(old, studentModel));
        //    await _context.SaveChangesAsync();
        //
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            return View();
        }
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Students == null)
        //        return BadRequest("Entity set 'ApplicationDbContext.Students'  is null.");
        //
        //    var studentModel = await _context.Students.FindAsync(id);
        //    if (studentModel != null)
        //    {
        //        _context.Students.Remove(studentModel);
        //        await _context.SaveChangesAsync();
        //    }
        //
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
