using Domain_Driven_Design.Domain.Argumentos.Usuario;
using Domain_Driven_Design.Domain.Entidades.Base;

namespace Domain_Driven_Design.Domain.Entidades.Usuario
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string NormalizedEmail { get; private set; }

        public string CPFouCNPJ { get; private set; }

        public byte[]? Imagem { get; private set; }

        public string Senha { get; private set; }

        public bool Claim { get; private set; }

        public string Token { get; private set; }

        public Usuario() { }

        public Usuario(UsuarioRequest request)
        {
            Nome = request.Nome!;
            Email = request.Email;
            NormalizedEmail = request.NormalizedEmail!;
            CPFouCNPJ = request.CPFouCNPJ;
            Senha = request.Senha;
            Imagem = request.Imagem;
            Claim = request.Claim;
            Token = request.Token!;
            Status = true;
        }

        public void Atualizar(UsuarioRequest request)
        {
            Nome = request.Nome
                ?? Nome;
            Email = request.Email
                ?? Email;
            NormalizedEmail = request.NormalizedEmail
                ?? NormalizedEmail;
            CPFouCNPJ = request.CPFouCNPJ
                ?? CPFouCNPJ;
            Imagem = request.Imagem
                ?? Imagem;
            Senha = request.Senha
                ?? Senha;
            Claim = request.Claim;
            Token = request.Token
                ?? Token;

            if (request.Status.HasValue) Status = request.Status.Value;
        }
    }
}
