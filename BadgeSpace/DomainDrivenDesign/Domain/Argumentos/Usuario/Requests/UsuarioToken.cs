using Domain_Driven_Design.Domain.Argumentos.Base;

namespace Domain_Driven_Design.Domain.Argumentos.Usuario.Requests
{
    public class UsuarioToken : ArgumentosBase
    {
        public string? Token { get; set; }

        public static explicit operator UsuarioToken(Domain_Driven_Design.Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioToken()
            {
                Token = entidade.Token,
            };
        }
    }
}
