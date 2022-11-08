using Domain.Argumentos.Base;

namespace Domain.Argumentos.Usuario
{
    public class UsuarioRequest : ArgumentosBase
    {
        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? CPFouCNPJ { get; set; }

        public byte[]? Imagem { get; set; }

        public string? Senha { get; set; }

        public string? ConfirmarSenha { get; set; }

        public bool Claim { get; set; }

        public string? Token { get; set; }
    }
}
