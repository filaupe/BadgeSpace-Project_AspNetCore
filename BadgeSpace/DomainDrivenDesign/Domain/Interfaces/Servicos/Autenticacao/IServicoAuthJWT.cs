namespace Domain_Driven_Design.Domain.Interfaces.Servicos.Autenticacao
{
    public interface IServicoAuthJWT
    {
        Task<string> GenerateToken(int Id, bool Claim, string Email, string CPFouCNPJ);
    }
}
