using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain_Driven_Design.Domain.Argumentos.Base;

namespace Domain_Driven_Design.Domain.Argumentos.Usuario.Requests
{
    public class UsuarioLogin : ArgumentosBase
    {
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
