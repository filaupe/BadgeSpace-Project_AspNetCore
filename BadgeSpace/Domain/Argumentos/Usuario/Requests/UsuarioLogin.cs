using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Argumentos.Base;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioLogin : ArgumentosBase
    {
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Isso não é um Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
