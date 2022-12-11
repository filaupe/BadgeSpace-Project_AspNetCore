using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BadgeSpace.API.Controllers.Empress
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpressController : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> GetEmpressAsync() => Ok("you");

        #region Student Crud Region
        [HttpGet("list-all-students")] public async Task<IActionResult> GetAllStudentsList() => Ok(); 
        [HttpGet("list-active-students")] public async Task<IActionResult> GetActiveStudentsList() => Ok(); 
        [HttpGet("list-deactive-students")] public async Task<IActionResult> GetDeactiveStudentsList() => Ok(); 

        [HttpGet("get-student-by-id")] public async Task<IActionResult> GetStudentByIdAsync([FromQuery] string? id) => Ok(); 
        [HttpGet("get-student-by-email-address")] public async Task<IActionResult> GetStudentByEmailAddressAsync([FromQuery] string? id) => Ok(); 

        [HttpPost("add-student")] public async Task<IActionResult> PostAddStudentAsync() => Ok(); 
        [HttpPost("add-students-by-csv")] public async Task<IActionResult> PostAddStudentsByTableArchiveAsync(IFormFile test)
        {
            var result = new StringBuilder();
            using var file = new StreamReader(test.OpenReadStream());
            string? line;

            while ((line = await file.ReadLineAsync()) != null)
                result.AppendLine(line);

            file.Close();

            return Ok(result.ToString());
        }
        
        [HttpPost("send-by-email-address-certificate-link-to-student")] public async Task<IActionResult> PostSendCertificateLinkToStudentAsync() => Ok(); 
        
        [HttpPut("edit-student-by-email-address")] public async Task<IActionResult> PutStudentByEmailAddressAsync() => Ok(); 
        [HttpPut("edit-student-by-id")] public async Task<IActionResult> PutStudentByIdAsync() => Ok(); 

        [HttpDelete("delete-student-by-email-address")] public async Task<IActionResult> DeleteStudentByEmailAddressAsync() => Ok(); 
        [HttpDelete("delete-student-by-id")] public async Task<IActionResult> DeleteStudentByIdAsync() => Ok();
        #endregion

        #region Certificate Crud Region
        [HttpGet("get-all-certificates")] public async Task<IActionResult> GetAllCertificatesAsync() => Ok(); 
        [HttpGet("get-active-certificates")] public async Task<IActionResult> GetActiveCertificatesAsync() => Ok();
        [HttpGet("get-deactive-certificates")] public async Task<IActionResult> GetDeactiveCertificatesAsync() => Ok();

        [HttpGet("get-certificate-by-identifier")] public async Task<IActionResult> GetCertificateByIdentifierAsync() => Ok();
        [HttpGet("get-certificate-by-id")] public async Task<IActionResult> GetCertificateByIdAsync() => Ok();

        [HttpPost("add-certificate")] public async Task<IActionResult> PostAddCertificateAsync() => Ok();
        [HttpPost("add-certificates-by-csv")] public async Task<IActionResult> PostAddCertificatesByTableArchiveAsync() => Ok();

        [HttpPut("edit-certificate-by-identifier")] public async Task<IActionResult> EditCertificateByIdentifierAsync() => Ok();
        [HttpPut("edit-certificate-by-id")] public async Task<IActionResult> EditCertificateByIdAsync() => Ok();

        [HttpDelete("delete-certificate-by-identifier")] public async Task<IActionResult> DeleteCertificateByIdentifierAsync() => Ok();
        [HttpDelete("delete-certificate-by-id")] public async Task<IActionResult> DeleteCertificateByIdAsync() => Ok();
        #endregion
    }
}