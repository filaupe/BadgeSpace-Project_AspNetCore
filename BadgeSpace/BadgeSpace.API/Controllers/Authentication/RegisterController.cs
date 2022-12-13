using BadgeSpace.API.Models;
using BadgeSpace.Infra;
using BadgeSpace.Models.User;
using BadgeSpace.Services.Authentication.JsonWebToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.API.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost] public async Task<IActionResult> PostRegister(
            [FromServices] AuthJWT jwt,
            [FromServices] ApplicationDbContext context, 
            [FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);
            await context.Users.AddAsync((UserModel)model);
            try
            {
                await context.SaveChangesAsync();
                return Created("/api/v1/Register", jwt.GenerateToken((UserModel)model));
            }
            catch
            {
                return BadRequest("Ocorreu um problema interno no sistema, tente novamente mais tarde.");
            }
        }

        [HttpGet("get-user")] public async Task<IActionResult> GetUser(
            [FromServices] ApplicationDbContext context,
            [FromQuery] string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(c => c.Email.ToUpper() == email.ToUpper());
            return Ok(user == null ? "O usuário não existe, crie" : $"O usuário {user.Email} existe!");
        }
    }
}