using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost] public async Task<IActionResult> PostLogin() => Ok();
     
        [HttpGet("login-methods")] public async Task<IActionResult> GetLoginMethods() => Ok("user");

        [HttpGet("account-type")] public async Task<IActionResult> GetAccountType() => Ok("user");
    }
}
