using Domain.Argumentos.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioEmail : ArgumentosBase
    {
        [DisplayName("Email")]
        public string EmailAntigo { get; set; }

        [DisplayName("Novo Email")]
        [Required(ErrorMessage = "A área Novo Email é obrigatória")]
        [Remote(action: "VerificarEmail", controller: "Validation")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Isso não é um Email")]
        public string Email { get; set; }

        public static explicit operator UsuarioEmail(Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioEmail()
            {
                EmailAntigo = entidade.Email,
            };
        }
    }
}
