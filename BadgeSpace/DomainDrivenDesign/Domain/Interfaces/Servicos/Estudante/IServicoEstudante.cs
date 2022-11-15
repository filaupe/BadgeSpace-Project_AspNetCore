using Domain_Driven_Design.Domain.Argumentos.Estudante;
using Domain_Driven_Design.Domain.Interfaces.Servicos.Base;

namespace Domain_Driven_Design.Domain.Interfaces.Servicos.Estudante
{
    public interface IServicoEstudante
    {
        Task<EstudanteResponse> Adicionar(EstudanteRequest request);
        EstudanteResponse Alterar(Domain_Driven_Design.Domain.Entidades.Estudante.Estudante old, EstudanteRequest request);
        EstudanteResponse Selecionar(int id);
        bool VerificarCodigo(string Codigo);
        IEnumerable<Domain_Driven_Design.Domain.Entidades.Estudante.Estudante> Listar(int skip, int take);
        IEnumerable<EstudanteResponse> ListarAtivos();
    }
}
