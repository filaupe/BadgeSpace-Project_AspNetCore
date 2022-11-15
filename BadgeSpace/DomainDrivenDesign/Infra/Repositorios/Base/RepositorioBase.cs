using Domain_Driven_Design.Domain.Entidades.Base;
using Domain_Driven_Design.Domain.Interfaces.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain_Driven_Design.Infra.Repositorios.Base
{
    public class RepositorioBase<TEntidade, TId> : IRepositorioBase<TEntidade, TId>
        where TEntidade : EntidadeBase
        where TId : struct
    {
        private readonly DbContext _context;

        public RepositorioBase(DbContext context) =>_context = context;

        public TEntidade Adicionar(TEntidade entidade) => _context.Set<TEntidade>().Add(entidade).Entity;

        public void Remover(TEntidade entidade) => _context.Set<TEntidade>().Remove(entidade);

        public bool Existe(Func<TEntidade, bool> where) => _context.Set<TEntidade>().Any(where);

        public IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades)
        {
            throw new NotImplementedException();
        }

        public TEntidade Editar(TEntidade entidade)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] icludeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] icludeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] icludeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] icludeProperties)
        {
            throw new NotImplementedException();
        }

        public TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] icludeProperties)
        {
            throw new NotImplementedException();
        }

        public TEntidade OrdenarPorId(TId id, params Expression<Func<TEntidade, object>>[] icludeProperties) => _context.Set<TEntidade>().Find(id)!;
    }
}
