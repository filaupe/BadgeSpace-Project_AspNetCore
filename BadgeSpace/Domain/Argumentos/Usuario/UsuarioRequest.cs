using Domain.Argumentos.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Argumentos.Usuario
{
    public class UsuarioRequest : ArgumentosBase
    {
        [DisplayName("Nome de Usuário")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A área Email é obrigatória")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Isso não é um Email")]
        public string Email { get; set; }

        public string? NormalizedEmail { get; set; }

        [Required(ErrorMessage = "A área de CPF ou CNPJ é obrigatória")]
        [DisplayName("CPF")]
        [RegularExpression("(^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$)|(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)", ErrorMessage = "A área não está de acordo com os padrões de formatação")]
        public string CPFouCNPJ { get; set; }

        public byte[]? Imagem { get; set; }

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A área Confirmar Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarSenha { get; set; }

        [DisplayName("Usuário / Empresa")]
        public bool Claim { get; set; }

        public string? Token { get; set; }
    }
}
