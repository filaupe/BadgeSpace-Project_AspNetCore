using Domain.Argumentos.Estudante;
using Domain.Interfaces.Servicos.Base;

namespace Domain.Interfaces.Servicos.Estudante
{
    public interface IServicoEstudante
    {
        Task<EstudanteResponse> Adicionar(EstudanteRequest request);
        EstudanteResponse Alterar(EstudanteRequest request);
        EstudanteResponse Selecioanr(int id);
        IEnumerable<Domain.Entidades.Estudante.Estudante> Listar(int skip, int take);
        IEnumerable<EstudanteResponse> ListarAtivos();
    }
}
