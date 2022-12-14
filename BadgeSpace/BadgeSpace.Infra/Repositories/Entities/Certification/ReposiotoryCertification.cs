using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Interfaces.Repository.Certification;
using BadgeSpace.Infra.Repositories.Entities.Base;

namespace BadgeSpace.Infra.Repositories.Entities.Certification
{
    public class ReposiotoryCertification : RepositoryBase<CertificationModel, int>, IRepositoryCertification
    {
        private readonly ApplicationDbContext _context;

        public ReposiotoryCertification(ApplicationDbContext context) : base(context) => _context = context;
    }
}
