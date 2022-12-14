using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Interfaces.Services.Entities.Certification;

namespace BadgeSpace.Infra.Services.Entities.Certification
{
    public class ServiceCertification : IServiceCertification
    {
        public Task<CertificationModel> AddAsync(CertificationModel entidades)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CertificationModel>> AddListAsync(IEnumerable<CertificationModel> entidades)
        {
            throw new NotImplementedException();
        }

        public void Remove(CertificationModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
