using Domain_Driven_Design.Domain.Argumentos.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Domain_Driven_Design.Domain.Argumentos.Usuario.Requests
{
    public class UsuarioEmail : ArgumentosBase
    {
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        public string EmailAntigo { get; set; }

        [DisplayName("Novo Email")]
        [Required(ErrorMessage = "A área Novo Email é obrigatória")]
        [Remote(action: "VerificarEmail", controller: "Validation")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        public string Email { get; set; }

        public static explicit operator UsuarioEmail(Domain_Driven_Design.Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioEmail()
            {
                EmailAntigo = entidade.Email,
            };
        }
    }
}
