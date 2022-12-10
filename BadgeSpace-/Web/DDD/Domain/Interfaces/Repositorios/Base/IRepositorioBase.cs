using System.Linq.Expressions;

namespace Domain_Driven_Design.Domain.Interfaces.Repositorios.Base
{
    public interface IRepositorioBase<TEntidade, in TId> where TEntidade : class where TId : struct
    {
        TEntidade Adicionar(TEntidade entidades);

        IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades);

        void Remover(TEntidade entidade);

        TEntidade Editar(TEntidade entidade);

        bool Existe(Func<TEntidade, bool> where);

        IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] icludeProperties);

        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] icludeProperties);

        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] icludeProperties);
        
        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] icludeProperties);

        TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] icludeProperties);

        TEntidade OrdenarPorId(TId id, params Expression<Func<TEntidade, object>>[] icludeProperties);
    }
}
