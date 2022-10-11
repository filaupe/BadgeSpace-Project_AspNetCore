using BadgeSpace.Data;
using BadgeSpace.Data.Migrations;
using BadgeSpace.Models;
using BadgeSpace.Models.Enums;
using BadgeSpace.Utils.Security;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Extensions
{
    // falta criar uma função pra reduzir a repetição do código
    [Controller]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context) => _context = context;

        [HttpGet("listar/{email}/{CPF?}/{ID?}")]
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

        [HttpGet("{CPF}/{empresa?}/{curso?}")]
        [Authorize(Roles = nameof(Roles.STUDENT))]
        public async Task<IActionResult> ListarCertificados(string CPF, string? empresa, string? curso)
        {
            var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);
            if (confirmC == null)
                return NotFound(new { message = "Inválido." });
            if (empresa != null)
            {
                var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
                if (confirmE == null)
                    return NotFound(new { message = "Inválido." });
            }
                
            if (curso != null && curso != "")
            {
                var student = _context.Students.Where(c => c.EmpresaId == empresa && c.AlunoCPF == CPF && c.Curso == curso);
                return Ok(student);
            }

            if (empresa != null && empresa != "")
            {
                var student = _context.Students.Where(c => c.EmpresaId == empresa && c.AlunoCPF == CPF);
                return Ok(student);
            }

            var Student = _context.Students.Where(c => c.AlunoCPF == CPF);

            return Ok(Student);
        }

        [HttpPost("{empresa}/cadastrar={CPF}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Cadastrar(string CPF, string empresa, [FromBody] StudentModel Student)
        {
            var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
            var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);

            if (confirmC == null || confirmE == null)
                return NotFound(new { message = "Inválido." });

            Student.AlunoCPF = CPF;
            Student.EmpresaId = empresa;

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return Created("Aluno", Student);
        }

        [HttpPut("{empresa}/editar={CPF}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Editar(string CPF, string empresa, int? ID, [FromBody] StudentModel? Student)
        {
            var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
            var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);

            if (confirmC == null || confirmE == null)
                return NotFound(new { message = "Inválido." });

            if (Student!.EmpresaId != empresa)
                return NotFound(new { message = "Aluno Inválido." });

            if (Student!.AlunoCPF != CPF)
                return NotFound(new { message = "Aluno Inválido." });

            if (ID.HasValue && ID != 0)
                if (Student.EmpresaId != empresa)
                    return NotFound(new { message = "Aluno Inválido." });

            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }

        [HttpDelete("{empresa}/deletar={CPF}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Remover(string empresa, string CPF, int? ID)
        {
            var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
            var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);

            if (confirmC == null || confirmE == null)
                return NotFound(new { message = "Inválido." });

            var student = ID.HasValue && ID != 0 
                ? await _context.Students.FirstOrDefaultAsync(c => c.Id == ID) 
                : await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF && c.EmpresaId == empresa);
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