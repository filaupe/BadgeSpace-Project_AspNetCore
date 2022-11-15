using Domain_Driven_Design.Domain.Interfaces.Servicos.Estudante;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Domain_Driven_Design.Infra.Recursos.Validacao
{
    public class Validation : Controller
    {
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IServicoEstudante _servicoEstudante;

        public Validation(IServicoUsuario servicoUsuario, IServicoEstudante servicoEstudante)
        {
            _servicoUsuario = servicoUsuario;
            _servicoEstudante = servicoEstudante;
        }

        public IActionResult VerificarEmail(string Email) 
            => _servicoUsuario.VerificarEmail(Email) 
                ? Json("O Email já está em uso.") : Json(true);

        public IActionResult VerificarCPFouCNPJ(string CPFouCNPJ)
            => _servicoUsuario.VerificarCPFouCNPJ(CPFouCNPJ)
                ? Json($"O CPF ou CNPJ já está em uso.") : Json(true);

        public IActionResult VerificarSenha(string Senha, string SenhaAtual)
            => Senha != SenhaAtual ? Json($"Está senha não é a sua atual") : Json(true);

        public IActionResult VerificarCodigo(string Codigo)
            => _servicoEstudante.VerificarCodigo(Codigo)
                ? Json($"Este código já foi inserido") : Json(true);
    }
}