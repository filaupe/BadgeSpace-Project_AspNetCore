using Domain.Argumentos.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioEmail : ArgumentosBase
    {
        [DisplayName("Email")]
        public string EmailAntigo { get; set; }

        [DisplayName("Novo Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Isso não é um Email")]
        public string NovoEmail { get; set; }

        public static explicit operator UsuarioEmail(Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioEmail()
            {
                EmailAntigo = entidade.Email,
            };
        }
    }
}
