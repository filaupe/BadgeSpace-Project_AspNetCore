using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.Student
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> GetStudentAsync() => Ok("you");

        [HttpGet("list-certificates")] public async Task<IActionResult> GetCertificatesListAsync() => Ok();
        
        [HttpGet("get-certificate-by-code")] public async Task<IActionResult> GetCertificateByCodeAsync() => Ok();
        [HttpGet("get-certificate-by-id")] public async Task<IActionResult> GetCertificateByIdAsync() => Ok();
    }
}
