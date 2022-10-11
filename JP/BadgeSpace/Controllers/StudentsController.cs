using BadgeSpace.Data;
using BadgeSpace.Models;
using BadgeSpace.Utils.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var (logado, _) = CheckIfUserIsValid.IsUserValid(_context.Users, User);

            if (!logado) return Unauthorized();

            return _context.Students != null ? View(await _context.Students.ToListAsync()) : BadRequest("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !id.HasValue) return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null) return NotFound();

            return View(studentModel);
        }

        // GET: Students/Create
        public IActionResult Create() => View();

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? Imagem, StudentModel studentModel)
        {
            var ok = await CheckIfUserIsValid.IsUserValid(_context.Users, studentModel.AlunoCPF);
            if (!ok || !ModelState.IsValid)
            {
                ViewBag.AuthCPF = "Este CPF não existe";
                return View(studentModel);
            }

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                studentModel.Imagem = memoryStream.ToArray();
            }

            _context.Add(studentModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue || id.Value == 0)
                return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentModel == null) return NotFound();

            return View(studentModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? Imagem, StudentModel studentModel)
        {
            if (id != studentModel.Id) return NotFound();

            var ok = await CheckIfUserIsValid.IsUserValid(_context.Users, studentModel.AlunoCPF);
            if (!ok || !ModelState.IsValid)
            {
                ViewBag.AuthCPF = "Este CPF não existe";
                return View(studentModel);
            }

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                studentModel.Imagem = memoryStream.ToArray();
            }

            var old = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentModel.Id);

            if (old == null) return BadRequest();

            _context.Update(OldToNewRegister(old, studentModel));
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !id.HasValue || id.Value == 0)
                return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null) return NotFound();

            return View(studentModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
                return BadRequest("Entity set 'ApplicationDbContext.Students'  is null.");

            var studentModel = await _context.Students.FindAsync(id);
            if (studentModel != null)
            {
                _context.Students.Remove(studentModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #region Aux methods
        private static StudentModel OldToNewRegister(StudentModel oldRegister, StudentModel newRegister)
        {
            oldRegister.Id = newRegister.Id;
            oldRegister.NomeAluno = newRegister.NomeAluno;
            oldRegister.AlunoCPF = newRegister.AlunoCPF;
            oldRegister.Curso = newRegister.Curso;
            oldRegister.Tipo = newRegister.Tipo;
            oldRegister.Nivel = newRegister.Nivel;
            oldRegister.Tempo = newRegister.Tempo;
            oldRegister.Descricao = newRegister.Descricao;
            oldRegister.Imagem = newRegister.Imagem;
            oldRegister.Habilidades = newRegister.Habilidades;
            oldRegister.EmpresaId = newRegister.EmpresaId;

            return oldRegister;
        }

        #endregion
    }
}
