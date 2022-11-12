namespace Domain.Interfaces.Servicos.Autenticacao
{
    public interface IServicoAuthJWT
    {
        Task<string> GenerateToken(int Id, bool Claim, string Email);
    }
}
