﻿using Web.Data;
using Web.Data.Migrations;
using Web.Models;
using Web.Models.Enums;
using Web.Utils.MethodsExtensions.UserCase;
using Web.Utils.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Utils.MethodsExtensions.AddMethods.Interfaces;

namespace Web.Extensions.API.Controllers
{
    // fazer as áreas se auto validarem
    [Controller]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMethods _Method;

        public AlunosController(ApplicationDbContext context, IMethods Method)
        {
            _context = context;
            _Method = Method;
        }
            
        
        [HttpGet("listar&{email}/{CPF?}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Listar(string email, string? CPF, int? ID)
        {
            var dados = await _Method.Get(_context.Students, ID, email, CPF)
                .Select(c => new { c.Id, c.NomeAluno, c.AlunoCPF, c.Curso })
                .ToListAsync();

            return Ok(dados);
        }

        [HttpGet("{CPF}/{empresa?}")]
        [Authorize(Roles = nameof(Roles.STUDENT))]
        public async Task<IActionResult> ListarCertificados(string CPF, string? empresa)
        {
            var dados = await _Method.Get(_context.Students, null, CPF, empresa).ToListAsync();

            return Ok(dados);
        }

        [HttpPost("{empresa}&cadastrar={CPF}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Cadastrar(string CPF, string empresa, [FromBody] StudentModel Student)
        {
            var Verify = await VerifyDataAsync(empresa, CPF);
            if (Verify == null)
            {
                Student.AlunoCPF = CPF;
                Student.EmpresaId = empresa;

                _context.Students.Add(Student);
                await _context.SaveChangesAsync();

                return Created("Aluno", Student);
            }
            return Verify;
        }

        [HttpPut("{empresa}&editar={CPF}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Editar(string CPF, string empresa, [FromBody] StudentModel NewStudent)
        {
            NewStudent.AlunoCPF = CPF;
            NewStudent.EmpresaId = empresa;

            var OldStudent = await FindStudentIdAsync(NewStudent.Id);

            if (OldStudent == null)
                return BadRequest(new { message = "O aluno não existe" });
            if (OldStudent.EmpresaId != NewStudent.EmpresaId)
                return BadRequest(new { message = "Empresa incorreta" });
            if (OldStudent.AlunoCPF != NewStudent.AlunoCPF)
                return BadRequest(new { message = "CPF incorreto" });

            _context.Students.Update(UserCaseExtension.OldToNewRegister(OldStudent, NewStudent));
            await _context.SaveChangesAsync();
            return Ok(NewStudent);
        }

        [HttpDelete("{empresa}&deletar={CPF}/{ID?}")]
        [Authorize(Roles = nameof(Roles.EMPRESS))]
        public async Task<IActionResult> Remover(string empresa, string CPF, int? ID)
        {
            var Verify = await VerifyDataAsync(empresa, CPF);
            if (Verify == null)
            {
                var student = ID.HasValue && ID != 0
                ? await FindStudentIdAsync(ID.Value)
                : await _context.Students.FirstOrDefaultAsync(c => c.AlunoCPF == CPF && c.EmpresaId == empresa);
                if (student == null)
                    return NotFound(new { message = "Aluno Inválido." });

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Aluno Deletado." });
            }
            return Verify;
        }

        #region Private Methods
        private async Task<NotFoundObjectResult?> VerifyDataAsync(string empresa, string CPF)
        {
            var confirmE = await _context.Users.FirstOrDefaultAsync(c => c.Email == empresa);
            var confirmC = await _context.Users.FirstOrDefaultAsync(c => c.CPF_CNPJ == CPF);

            if (confirmC == null || confirmE == null)
                return NotFound(new { message = "Empresa ou CPF não encontrados no sistema" });
            return null;
        }
        private async Task<StudentModel?> FindStudentIdAsync(int id)
            => await _context.Students.FirstOrDefaultAsync(c => c.Id == id);
        #endregion
    }
}