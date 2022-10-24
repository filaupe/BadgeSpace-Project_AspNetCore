using Web.Models;
using Web.Models.Enums;
using Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private readonly IConfiguration _configuration;

        public ApiAuthService(IConfiguration configuration) => _configuration = configuration;

        public async Task<string> GenerateToken(ApplicationUser appUser)
            => await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value);

                // Configs
                var isAdm = appUser.Empresa ? nameof(Roles.EMPRESS) : nameof(Roles.STUDENT);

                // Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                            new Claim(ClaimTypes.Sid, appUser.Id),
                            new Claim(ClaimTypes.Role, isAdm)
                   }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            });

    }
}