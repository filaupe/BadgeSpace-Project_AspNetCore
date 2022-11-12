using Domain.Argumentos.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioSenha : ArgumentosBase
    {
        public UsuarioSenha() { }
        public UsuarioSenha(Domain.Entidades.Usuario.Usuario entidade) => this.SenhaAntiga = entidade.Senha;

        public string SenhaAntiga { get; set; }

        [Compare(nameof(SenhaAntiga))]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "A área Confirmar Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarSenha { get; set; }
    }
}
