using BadgeSpace.Data;
using BadgeSpace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Extensions
{
    [Controller]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("alunos")]
        public async Task<ActionResult> cadastrar([FromBody] StudentModel Student)
        {
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return Created("Aluno", Student);
        }

        [HttpGet("alunos")]
        public async Task<ActionResult> listar()
        {
            var dados = await _context.Students.ToListAsync();
            return Ok(dados);
        }

        [HttpPut("alunos")]
        public async Task<ActionResult> editar([FromBody] StudentModel Student)
        {
            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }

        [HttpDelete("alunos/{codigo}")]
        public async Task<ActionResult> remover(int codigo)
        {
            StudentModel Student = filtrar(codigo);
            if (Student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("alunos/{codigo}")]
        public StudentModel filtrar(int codigo)
        {
            StudentModel Student = _context.Students.Find(codigo);
            return Student;
        }
    }
}