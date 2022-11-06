using Domain.Argumentos.Base;

namespace Domain.Argumentos.Usuario
{
    public class UsuarioResponse : ArgumentosBase
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPFouCNPJ { get; set; }

        public byte[]? Imagem { get; set; }

        public string Senha { get; set; }

        public bool Claim { get; set; }

        public string Token { get; set; }

        public static explicit operator UsuarioResponse(Domain.Entidades.Usuario.Usuario entidade)
        {
            return new UsuarioResponse()
            {
                Nome = entidade.Nome,
                Email = entidade.Email,
                CPFouCNPJ = entidade.CPFouCNPJ,
                Token = entidade.Token,
                Claim = entidade.Claim,
                Imagem = entidade.Imagem,
            };
        }
    }
}
