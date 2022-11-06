using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Interfaces.Servicos.Autenticacao;
using Domain.Argumentos.Usuario;
using Domain.Recurso.Enums;

namespace Domain.Servicos.Autenticacao
{
    public class AuthJWT : IServicoAuthJWT
    {
        private readonly IConfiguration _configuration;

        public AuthJWT(IConfiguration configuration) => _configuration = configuration;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateToken(UsuarioRequest request)
            => await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value);

                // Configs
                var isEmpress = request.Claim ? nameof(Roles.Empresa) : nameof(Roles.Usuario);

                // Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                            new Claim(ClaimTypes.Sid, request.Id.ToString()),
                            new Claim(ClaimTypes.Email, request.Email),
                            new Claim(ClaimTypes.Role, isEmpress)
                   }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            });
    }
}
