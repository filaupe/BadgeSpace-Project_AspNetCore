using Microsoft.AspNetCore.Mvc;
using Infra.Recursos.Validacao;
using Infra;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class ValidationController : Controller
    {
        private readonly Validation _validation;
        private readonly ApplicationDbContext _context;
       
        public ValidationController(Validation validation, ApplicationDbContext context)
        {
            _validation = validation;
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarEmail([FromQuery] string Email) => _validation.VerificarEmail(Email);

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarCPFouCNPJ([FromQuery] string CPFouCNPJ) => _validation.VerificarCPFouCNPJ(CPFouCNPJ);

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerificarSenha([FromQuery] string Senha) => _validation.VerificarSenha(Senha, (await _context.Usuarios.FirstOrDefaultAsync(u => u.CPFouCNPJ == User.Claims.ToList()[2].Value))!.Senha);

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarCodigo([FromQuery] string Codigo) => _validation.VerificarCodigo(Codigo);
    }
}
