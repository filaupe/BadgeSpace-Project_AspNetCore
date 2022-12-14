using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Repository.User;
using BadgeSpace.Infra.Repositories.Entities.Base;

namespace BadgeSpace.Infra.Repositories.Entities.User
{
    public class ReposiotoryUser : RepositoryBase<UserModel, int>, IRepositoryUser
    {
        private readonly ApplicationDbContext _context;

        public ReposiotoryUser(ApplicationDbContext context) : base(context) => _context = context;
    }
}
