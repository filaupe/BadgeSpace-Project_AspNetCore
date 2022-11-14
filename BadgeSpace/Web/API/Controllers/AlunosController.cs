using Microsoft.AspNetCore.Mvc;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Domain.Recursos.Enums;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Servicos.Estudante;
using Domain.Argumentos.Estudante;
using AutoMapper;
using Domain.Interfaces.Repositorios.Estudante;

namespace Web.Extensions.API.Controllers
{
    [ApiController]
    [Route("api/v1/gerenciamento")]
    public class AlunosController : Controller
    {
        public string userData { get => User.Claims.ToArray()[1].Value; }

        [HttpGet("listar/alunos")]
        [Authorize(Roles = nameof(Roles.EMPRESA))]
        public async Task<IActionResult> ListarAlunos(
            [FromServices] ApplicationDbContext context,
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 25)
        {
            var where = context.Estudantes.Where(s => s.Empresa!.CPFouCNPJ == userData);
            
            var total = (await where.CountAsync()).ToString();
            var estudantes = (await where.AsNoTracking().Skip(skip).Take(take).ToListAsync());

            return Ok(new 
            { 
                count = total,
                data = estudantes
            });
        }

        [HttpGet("listar/certificados")]
        [Authorize(Roles = nameof(Roles.USUARIO))]
        public async Task<IActionResult> ListarCertificados(
        [FromServices] ApplicationDbContext context,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 25)
        {
            var where = context.Estudantes.Where(s => s.CPF == userData);

            var total = (await where.CountAsync()).ToString();
            var certificados = (await where.AsNoTracking().Skip(skip).Take(take).ToListAsync());

            return Ok(new
            {
                count = total,
                data = certificados
            });
        }

        [HttpPost("cadastrar")]
        [Authorize(Roles = nameof(Roles.EMPRESA))]
        public async Task<IActionResult> CadastrarAluno(
            [FromServices] ApplicationDbContext context,
            [FromServices] IServicoEstudante service,
            [FromForm] IFormFile Imagem,
            [FromForm] EstudanteRequest request)
        {
            request.Empresa = await context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == userData);

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                request.Imagem = memoryStream.ToArray();
            }
            if (ModelState.IsValid)
            {
                var response = await service.Adicionar(request);
                await context.SaveChangesAsync();

                return Created("Aluno", response);
            }
            return BadRequest(request);
        }

        [HttpPut("editar&id={id:int}")]
        [Authorize(Roles = nameof(Roles.EMPRESA))]
        public async Task<IActionResult> EditarAluno(
            [FromServices] ApplicationDbContext context,
            [FromServices] IServicoEstudante service,
            [FromServices] IRepositorioEstudante repositorio,
            [FromRoute] int id,
            [FromForm] IFormFile Imagem,
            [FromForm] EstudanteRequest request)
        {
            request.Empresa = await context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == userData);

            if (Imagem != null)
            {
                using var memoryStream = new MemoryStream();
                await Imagem.CopyToAsync(memoryStream);
                request.Imagem = memoryStream.ToArray();
            }
            if (ModelState.IsValid)
            {
                var response = service.Alterar(repositorio.OrdenarPorId(id), request);
                await context.SaveChangesAsync();
                return Ok(response);
            }
            return BadRequest(request);
        }

        [HttpDelete("deletar&id={id:int}")]
        [Authorize(Roles = nameof(Roles.EMPRESA))]
        public async Task<IActionResult> DeletarAluno(
            [FromServices] ApplicationDbContext context,
            [FromServices] IRepositorioEstudante repositorio,
            [FromRoute] int id)
        {
            if (repositorio.Existe(u => u.Id == id))
            {
                repositorio.Remover((await context.Estudantes.FindAsync(id))!);
                await context.SaveChangesAsync();
                return Ok("Deletado");
            }
            return BadRequest($"O id:{id} não existe");
        }
    }
}