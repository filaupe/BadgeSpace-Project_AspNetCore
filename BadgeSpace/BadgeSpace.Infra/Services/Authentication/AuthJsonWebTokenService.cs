using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
using BadgeSpace.Domain.Resources.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BadgeSpace.Infra.Services.Authentication
{
    public class AuthJsonWebTokenService : IAuthJsonWebTokenService
    {
        private readonly IConfiguration _configuration;

        public AuthJsonWebTokenService(IConfiguration configuration) => _configuration = configuration;

        public async Task<string> GenerateToken(UserModel user)
            => await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value!);

                // Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.NormalizedEmail),
                        new Claim(ClaimTypes.Role, user.Claim ? nameof(Roles.EMPRESA) : nameof(Roles.ESTUDANTE))
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            });
    }
}
