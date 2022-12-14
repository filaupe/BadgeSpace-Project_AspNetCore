using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Services.Entities.Base;

namespace BadgeSpace.Domain.Interfaces.Services.Entities.User
{
    public interface IServiceUser : IServiceBase<UserModel, int>
    {
        Task<bool> VerifyEmailAdress(string Email);
    }
}
