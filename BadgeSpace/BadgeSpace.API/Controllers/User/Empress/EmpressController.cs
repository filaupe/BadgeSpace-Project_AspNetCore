using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.API.Controllers.Empress
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpressController : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> GetEmpressAsync() => Ok("you");

        [HttpGet("list-all-students")] public async Task<IActionResult> GetAllStudentsList() => Ok(); 
        [HttpGet("list-active-students")] public async Task<IActionResult> GetActiveStudentsList() => Ok(); 

        [HttpGet("get-student-by-id")] public async Task<IActionResult> GetStudentByIdAsync([FromQuery] string? id) => Ok(); 
        [HttpGet("get-student-by-email-address")] public async Task<IActionResult> GetStudentByEmailAddressAsync([FromQuery] string? id) => Ok(); 

        [HttpPost("add-student")] public async Task<IActionResult> PostStudentAsync() => Ok(); 
        [HttpPost("add-students-by-file")] public async Task<IActionResult> PostStudentsByTableArchiveAsync() => Ok(); 
        
        [HttpPost("add-certificate")] public async Task<IActionResult> PostCertificateAsync() => Ok(); 
        [HttpPost("add-certificate-by-file")] public async Task<IActionResult> PostCertificatesByTableArchiveAsync() => Ok(); 
        
        [HttpPost("set-primary-certificate")] public async Task<IActionResult> PostSetPrimaryCertificateAsync() => Ok(); 

        [HttpPut("edit-student-by-email-address")] public async Task<IActionResult> PutStudentByEmailAddressAsync() => Ok(); 
        [HttpPut("edit-student-by-id")] public async Task<IActionResult> PutStudentByIdAsync() => Ok(); 

        [HttpDelete("delete-student-by-email-address")] public async Task<IActionResult> DeleteStudentByEmailAddressAsync() => Ok(); 
        [HttpDelete("delete-student-by-id")] public async Task<IActionResult> DeleteStudentByIdAsync() => Ok(); 
    }
}