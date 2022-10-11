using BadgeSpace.Data;
using BadgeSpace.Models;
using BadgeSpace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Extensions
{
    [Controller]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IApiAuthService _apiAuthService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IApiAuthService apiAuthService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _apiAuthService = apiAuthService;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] ApiLoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == model.Login);
            if (user == null)
                return NotFound(new { message = "Email não encontrado." });

            var validPass = await _userManager.CheckPasswordAsync(user, model.Senha);

            if (!validPass)
                return NotFound(new { message = "Senha Incorreta." });


            var token = await _apiAuthService.GenerateToken(user);

            return Ok(token);
        }
    }
}
