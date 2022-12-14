using BadgeSpace.Domain.Interfaces.Repository.User;
using BadgeSpace.Domain.Interfaces.Services.Entities.User;

namespace BadgeSpace.Infra.Services.Entities.User
{
    public class ServiceUser : IServiceUser
    {
        private readonly IRepositoryUser _repository;

        public ServiceUser(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<bool> VerifyEmailAdress(string Email)
            => await _repository.AnyAsync(c => c.NormalizedEmail == Email.ToUpper());
    }
}
