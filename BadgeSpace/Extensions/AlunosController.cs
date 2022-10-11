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

        [HttpGet("email={email}/{CPF?}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Listar(string email, string? CPF, int? ID)
        {
            if (ID.HasValue && ID != 0)
            {
                var student = await _context.Students.FirstOrDefaultAsync(c => c.Id == ID);
                if (student == null) return NotFound(new { message = "Aluno Inválido." });

                return Ok(new { student.Id, student.NomeAluno, student.AlunoCPF, student.Curso });
            }
                

            if (CPF != null && CPF != "")
            {
                var students = _context.Students
                    .Where(c => c.AlunoCPF == CPF && c.EmpresaId == email)
                    .Select(c => new { c.Id, c.NomeAluno, c.AlunoCPF, c.Curso } );
                if (students == null) return NotFound(new { message = "Aluno Inválido." });

                return Ok(students);
            }

            var dados = _context.Students
                .Where( c => c.EmpresaId == email )
                .Select( c => new { c.Id, c.NomeAluno, c.AlunoCPF, c.Curso });

            if (dados == null) return NotFound(new { message = "A lista está vazia." });

            return Ok(dados);
        }

        [HttpGet("{CPF}")]
        [Authorize(Roles = nameof(Roles.STUDENT))]
        public IActionResult ListarCertificados(string? CPF)
        {
            if (CPF != null)
            {
                var student = _context.Students.Where(x => x.AlunoCPF == CPF);
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

        [HttpDelete("deletar={CPF}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Remover(string CPF, int? ID)
        {
            var student = ID.HasValue && ID != 0 
                ? await _context.Students.FirstOrDefaultAsync(c => c.Id == ID) : await filtrar(CPF);
            if (student == null) return NotFound(new { message = "Aluno Inválido." });

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Aluno Deletado." });
        }

        #region Private Methods
        private async Task<StudentModel?> filtrar(string CPF) 
            => await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF);
        #endregion
    }
}