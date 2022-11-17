﻿using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Autenticacao;
using Domain_Driven_Design.Domain.Recursos.Enums;

namespace Domain_Driven_Design.Infra.Servicos.Autenticacao
{
    public class AuthJWT : IServicoAuthJWT
    {
        private readonly IConfiguration _configuration;

        public AuthJWT(IConfiguration configuration) => _configuration = configuration;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateToken(int Id, bool Claim, string Email, string CPFouCNPJ)
            => await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value);

                // Configs
                var isEmpress = Claim ? nameof(Roles.EMPRESA) : nameof(Roles.USUARIO);

                // Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                            new Claim(ClaimTypes.Sid, Id.ToString()),
                            new Claim(ClaimTypes.Name, CPFouCNPJ),
                            new Claim(ClaimTypes.Email, Email),
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