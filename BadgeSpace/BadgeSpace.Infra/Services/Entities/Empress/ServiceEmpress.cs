using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Interfaces.Services.Entities.Empress;

namespace BadgeSpace.Infra.Services.Entities.Empress
{
    public class ServiceEmpress : IServiceEmpress
    {
        public Task<EmpressModel> AddAsync(EmpressModel entidades)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmpressModel>> AddListAsync(IEnumerable<EmpressModel> entidades)
        {
            throw new NotImplementedException();
        }

        public void Remove(EmpressModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
