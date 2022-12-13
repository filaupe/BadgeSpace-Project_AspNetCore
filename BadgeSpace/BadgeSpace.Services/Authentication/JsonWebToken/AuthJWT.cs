using BadgeSpace.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BadgeSpace.Services.Authentication.JsonWebToken
{
    public class AuthJWT
    {
        private readonly IConfiguration _configuration;

        public AuthJWT(IConfiguration configuration) => _configuration = configuration;

        public async Task<string> GenerateToken(UserModel user)
            => await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value!);

                // Configs
                var isEmpress = user.Claim ? "Empresa" : "Estudante";

                // Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, isEmpress)
                   }),
                    Expires = null,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            });
    }
}
