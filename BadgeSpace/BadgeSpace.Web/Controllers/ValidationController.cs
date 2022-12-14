using BadgeSpace.Infra;
using BadgeSpace.Infra.Resources.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ValidationMethods _validation;
        private readonly ApplicationDbContext _context;

        public ValidationController(ValidationMethods validation, ApplicationDbContext context)
        {
            _validation = validation;
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerificarEmail([FromQuery] string Email) => await _validation.VerifyEmailAdress(Email);

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerificarSenha([FromQuery] string Password) 
            => _validation.VerifyPassword(Password, (await _context.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == User.Claims.ToList()[2].Value))!.Password);

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarCodigo([FromQuery] string Code) => _validation.VerifyCode(Code); // pegar no mkleads
    }
}
