using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadgeSpace.Data;
using BadgeSpace.Models;
using Microsoft.AspNetCore.Authorization;

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
            var identidy = false;
            foreach (var item in _context.Users)
            {
                if (item.Email == User.Identity.Name)
                {
                    identidy = item.Empresa;
                }
            }
            return _context.Students != null ?
                    View(await _context.Students.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string NomeAluno, string AlunoCPF, string Curso, string Tipo, string Nivel, string Tempo, string Descricao, IFormFile Imagem, string Habilidades, string EmpresaId, StudentModel studentModel)
        {
            var ok = 0;
            foreach (var item in _context.Users)
            {
                if (item.CPF == AlunoCPF)
                {
                    ok = 1;
                    break;
                }
            }
            if (ok == 1)
            {
                if (ModelState.IsValid)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Imagem.CopyToAsync(memoryStream);
                        var file = new StudentModel()
                        {
                            Id = Id,
                            NomeAluno = NomeAluno,
                            AlunoCPF = AlunoCPF,
                            Curso = Curso,
                            Tipo = Tipo,
                            Nivel = Nivel,
                            Tempo = Tempo,
                            Descricao = Descricao,
                            Imagem = memoryStream.ToArray(),
                            Habilidades = Habilidades,
                            EmpresaId = EmpresaId,
                        };
                        _context.Add(file);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewBag.AuthCPF = "Este CPF não existe";
            return View(studentModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return View(studentModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int Id, string NomeAluno, string AlunoCPF, string Curso, string Tipo, string Nivel, string Tempo, string Descricao, IFormFile Imagem, string Habilidades, StudentModel studentModel)
        {
            var ok = 0;
            if (id != studentModel.Id)
            {
                return NotFound();
            }
            foreach (var item in _context.Users)
            {
                if (item.CPF == AlunoCPF)
                {
                    ok = 1;
                    break;
                }
            }
            if (ok == 1)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await Imagem.CopyToAsync(memoryStream);
                            var file = new StudentModel()
                            {
                                Id = Id,
                                NomeAluno = NomeAluno,
                                AlunoCPF = AlunoCPF,
                                Curso = Curso,
                                Tipo = Tipo,
                                Nivel = Nivel,
                                Tempo = Tempo,
                                Descricao = Descricao,
                                Imagem = memoryStream.ToArray(),
                                Habilidades = Habilidades,
                            };
                            _context.Update(file);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentModelExists(studentModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(studentModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentModel = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var studentModel = await _context.Students.FindAsync(id);
            if (studentModel != null)
            {
                _context.Students.Remove(studentModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModelExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
