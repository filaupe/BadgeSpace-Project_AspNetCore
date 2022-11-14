using Domain.Argumentos.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioSenha : ArgumentosBase
    {
        [DisplayName("Senha Atual")]
        [Required(ErrorMessage = "A área Senha Atual é obrigatória")]
        [Remote(action: "VerificarSenha", controller: "Validation")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A área Nova Senha é obrigatória")]
        [DisplayName("Nova Senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "A área Confirmar Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarSenha { get; set; }
    }
}
