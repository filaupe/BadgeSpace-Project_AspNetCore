using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Interfaces.Repository.Empress;
using BadgeSpace.Infra.Repositories.Entities.Base;

namespace BadgeSpace.Infra.Repositories.Entities.Empress
{
    public class ReposiotoryEmpress : RepositoryBase<EmpressModel, int>, IRepositoryEmpress
    {
        private readonly ApplicationDbContext _context;

        public ReposiotoryEmpress(ApplicationDbContext context) : base(context) => _context = context;
    }
}
