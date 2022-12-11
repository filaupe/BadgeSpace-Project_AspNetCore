using BadgeSpace.API.Models;
using BadgeSpace.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost] public IActionResult PostRegister(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);
            return Ok((UserModel)model);
        }

        [HttpGet("get-user")] public IActionResult GetUser() => Ok("batata");
    }
}