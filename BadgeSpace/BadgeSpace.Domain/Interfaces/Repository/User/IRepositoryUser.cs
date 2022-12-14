using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Repository.Base;

namespace BadgeSpace.Domain.Interfaces.Repository.User
{
    public interface IRepositoryUser : IRepositoryBase<UserModel, int>
    {
    }
}
