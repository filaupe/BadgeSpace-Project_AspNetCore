using BadgeSpace.Data;
using BadgeSpace.Data.Migrations;
using BadgeSpace.Models;
using BadgeSpace.Models.Enums;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("email={email}/{CPF?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Listar(string email, string? CPF)
        {
            if (CPF != null && CPF != "")
            {
                var student = await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF);
                if (student == null) return NotFound(new { message = "Aluno Inválido." });

                return Ok(new { student.Id, student.AlunoCPF, student.Curso });
            }

            var dados = await _context.Students
                .Where( c => c.EmpresaId == email )
                .Select( c => new { c.Id, c.AlunoCPF, c.Curso } )
                .FirstOrDefaultAsync();

            if (dados == null) return NotFound(new { message = "A lista está vazia." });

            return Ok(dados);
        }

        [HttpGet("{CPF}")]
        [Authorize(Roles = nameof(Roles.STUDENT))]
        public async Task<IActionResult> ListarCertificados(string? CPF)
        {
            if (CPF != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF);
                if (student == null) return NotFound(new { message = "Aluno Inválido." });

                return Ok(student);
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Cadastrar([FromBody] StudentModel Student)
        {
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return Created("Aluno", Student);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Editar([FromBody] StudentModel Student)
        {
            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }

        [HttpDelete("deletar={codigo}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
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