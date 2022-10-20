using BadgeSpace.Data;
using BadgeSpace.Models;
using BadgeSpace.Models.Enums;
using BadgeSpace.Utils.MethodsExtensions.UserCase;
using BadgeSpace.Utils.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Controllers
{
    [Authorize(Roles = nameof(Roles.EMPRESS))]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context) => _context = context;


        public IActionResult Create() => View();

        public async Task<IActionResult> Index() => View(await _context.Students.ToListAsync());
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !id.HasValue || id.Value == 0)
                return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

            if (studentModel == null) 
                return NotFound();

            return View(studentModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !id.HasValue) 
                return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

            if (studentModel == null) 
                return NotFound();

            return View(studentModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue || id.Value == 0)
                return NotFound();

            var studentModel = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentModel == null) 
                return NotFound();

            return View(studentModel);
        }


        [HttpPost, ActionName("Create")]
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

        [HttpPost, ActionName("Edit")]
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

            _context.Update(UserCaseExtension.OldToNewRegister(old, studentModel));
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

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
    }
}
