using Microsoft.AspNetCore.Mvc;
using Infra;

namespace Web.Extensions.API.Controllers
{
    // fazer as áreas se auto validarem
    [Controller]
    [Route("api/[controller]")]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }
            
        //[HttpGet("listar&{email}/{CPF?}/{ID?}")]
        //[Authorize(Roles = nameof(Roles.EMPRESA))]
        //public async Task<IActionResult> Listar(string email, string? CPF, int? ID)
        //{
        //    if(ID.HasValue && ID != 0)
        //    {
        //        var Result = _Method.GetById(_context, ID.Value).Result!;
        //        return Ok(new { Result.Id, Result.NomeAluno, Result.Codigo, Result.AlunoCPF, Result.Curso });
        //    }
        //    return Ok(await _Method.Get(_context.Students, email, CPF!).Select(c => new { c.Id, c.NomeAluno, c.Codigo, c.AlunoCPF, c.Curso }).ToListAsync());
        //}

        //[HttpGet("{CPF}/{empresa?}")]
        //[Authorize(Roles = nameof(Roles.USUARIO))]
        //public async Task<IActionResult> ListarCertificados(string CPF, string? empresa) => Ok(await _Method.Get(_context.Students, CPF, empresa).ToListAsync());

        //[HttpPost("{empresa}&cadastrar={CPF}")]
        //[Authorize(Roles = nameof(Roles.USUARIO))]
        //public async Task<IActionResult> Cadastrar(string CPF, string empresa, [FromBody] StudentModel Student)
        //{
        //    var Verify = await VerifyDataAsync(empresa, CPF);
        //    if (Verify == null)
        //    {
        //        Student.AlunoCPF = CPF;
        //        Student.EmpresaId = empresa;
        //
        //        if (Student.Imagem == null)
        //            return BadRequest(new { message = "Imagem aceita apenas o tipo byte" });
        //
        //        _context.Students.Add(Student);
        //        await _context.SaveChangesAsync();
        //
        //        return Created("Aluno", Student);
        //    }
        //    return Verify;
        //}

        //[HttpPut("{empresa}&editar={CPF}")]
        //[Authorize(Roles = nameof(Roles.EMPRESA))]
        //public async Task<IActionResult> Editar(string CPF, string empresa, [FromBody] StudentModel NewStudent)
        //{
        //    NewStudent.AlunoCPF = CPF;
        //    NewStudent.EmpresaId = empresa;
        //
        //    var OldStudent = await FindStudentIdAsync(NewStudent.Id);
        //    
        //    if (OldStudent == null)
        //        return BadRequest(new { message = "O aluno não existe" });
        //    if (OldStudent.EmpresaId != NewStudent.EmpresaId)
        //        return BadRequest(new { message = "EMPRESA incorreta" });
        //    if (OldStudent.AlunoCPF != NewStudent.AlunoCPF)
        //        return BadRequest(new { message = "CPF incorreto" });
        //    if (OldStudent.Imagem != null)
        //        return BadRequest(new { message = "Imagem aceita apenas o tipo byte" });
        //
        //    _context.Students.Update(UserCaseExtension.OldToNewRegister(OldStudent, NewStudent));
        //    await _context.SaveChangesAsync();
        //    return Ok(NewStudent);
        //}

        //[HttpDelete("{empresa}&deletar={CPF}/{ID?}")]
        //[Authorize(Roles = nameof(Roles.EMPRESS))]
        //public async Task<IActionResult> Remover(string empresa, string CPF, int? ID)
        //{
        //    var Verify = await VerifyDataAsync(empresa, CPF);
        //    if (Verify == null)
        //    {
        //        var student = ID.HasValue && ID != 0
        //        ? await FindStudentIdAsync(ID.Value)
        //        : await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF && c.EmpresaId == empresa);
        //        if (student == null)
        //            return NotFound(new { message = "Aluno Inválido." });
        //
        //        _context.Students.Remove(student);
        //        await _context.SaveChangesAsync();
        //
        //        return Ok(new { message = "Aluno Deletado." });
        //    }
        //    return Verify;
        //}

        //#region Private Methods
        //private async Task<IActionResult?> VerifyDataAsync(string empresa, string CPF)
        //{
        //    var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
        //    var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);
        //
        //    if (confirmC == null || confirmE == null)
        //        return NotFound(new { message = "EMPRESA ou CPF não encontrados no sistema" });
        //    return null;
        //}
        //private async Task<StudentModel?> FindStudentIdAsync(int id)
        //    => await _context.Students.FirstOrDefaultAsync(c => c.Id == id);
        //#endregion
    }
}