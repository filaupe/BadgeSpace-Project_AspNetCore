using Domain.Argumentos.Base;

namespace Domain.Argumentos.Usuario.Requests
{
    public class UsuarioToken : ArgumentosBase
    {
        public string? Token { get; set; }

        public static explicit operator UsuarioToken(Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioToken()
            {
                Token = entidade.Token,
            };
        }
    }
}
