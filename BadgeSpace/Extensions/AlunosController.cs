using BadgeSpace.Data;
using BadgeSpace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Extensions
{
    [Controller]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] StudentModel Student)
        {
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return Created("Aluno", Student);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Listar(int? id)
        {
            if (id.HasValue && id.Value != 0)
            {
                var student = await _context.Students.FindAsync(id.Value);
                if (student == null) return NotFound();

                return Ok(new { student.Id, student.AlunoCPF, student.Curso });
            }

            var dados = await _context.Students.Select(c => new { c.Id, c.AlunoCPF, c.Curso }).ToListAsync();
            return Ok(dados);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] StudentModel Student)
        {
            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Remover(int codigo)
        {
            var Student = await filtrar(codigo);
            if (Student == null) return NotFound();

            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return Ok();
        }

        #region Private Methods
        private async Task<StudentModel?> filtrar(int codigo) => await _context.Students.FindAsync(codigo);
        #endregion
    }
}