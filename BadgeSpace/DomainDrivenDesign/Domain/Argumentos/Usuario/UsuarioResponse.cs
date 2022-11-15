using Domain_Driven_Design.Domain.Argumentos.Base;

namespace Domain_Driven_Design.Domain.Argumentos.Usuario
{
    public class UsuarioResponse : ArgumentosBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public string CPFouCNPJ { get; set; }

        public byte[]? Imagem { get; set; }

        public string Senha { get; set; }

        public bool Claim { get; set; }

        public string Token { get; set; }

        public static explicit operator UsuarioResponse(Domain_Driven_Design.Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioResponse()
            {
                Id = entidade.Id,
                Email = entidade.Email,
                CPFouCNPJ = entidade.CPFouCNPJ,
            };
        }
    }
}
