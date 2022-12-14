using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Base;

namespace BadgeSpace.Domain.Interfaces.Repository.Student
{
    public interface IRepositoryStudent : IRepositoryBase<StudentModel, int>
    {
    }
}
