using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost] public IActionResult PostRegister() => Ok("batata");

        [HttpGet("get-user")] public IActionResult GetUser() => Ok("batata");
    }
}