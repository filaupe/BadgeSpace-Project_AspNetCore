using Domain_Driven_Design.Domain.Argumentos.Base;
using Domain_Driven_Design.Domain.Recursos.CustomValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain_Driven_Design.Domain.Argumentos.Usuario
{
    public class UsuarioRequest : ArgumentosBase
    {
        [DisplayName("Nome de Usuário")]
        public string? Nome { get; set; }

        [Remote(action: "VerificarEmail", controller: "Validation")]
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        [DisplayName("Email")]
        //[EmailExist()]
        public string Email { get; set; } = string.Empty;

        public string NormalizedEmail { get; set; } = string.Empty;

        [Remote(action: "VerificarCPFouCNPJ", controller: "Validation")]
        [Required(ErrorMessage = "A área de CPF ou CNPJ é obrigatória")]
        [DisplayName("CPF")]
        [RegularExpression("(^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$)|(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)", ErrorMessage = "A área não está de acordo com os padrões de formatação")]
        public string CPFouCNPJ { get; set; } = string.Empty;

        public byte[]? Imagem { get; set; }

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "A área deve haver ao menos 8 caracteres")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A área Confirmar Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [DisplayName("Usuário / Empresa")]
        public bool Claim { get; set; }

        public string? Token { get; set; }
    }
}
