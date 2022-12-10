using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> GetUserAsync() => Ok();

        [HttpPost("change-user-image")] public async Task<IActionResult> PostChangeUserImageAsync() => Ok();

        [HttpPut("change-username")] public async Task<IActionResult> PutChangeUserName() => Ok();
        [HttpPut("change-password")] public async Task<IActionResult> PutChangePassword() => Ok();
        [HttpPut("change-email-address")] public async Task<IActionResult> PutChangeEmailAddress() => Ok();

        [HttpGet("change-api-key")] public async Task<IActionResult> GetChangeApiKey() => Ok();

        [HttpDelete("delete-account")] public async Task<IActionResult> DeleteAccountAsync() => Ok();
    }
}
