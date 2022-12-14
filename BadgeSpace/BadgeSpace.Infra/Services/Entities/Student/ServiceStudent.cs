using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Student;

namespace BadgeSpace.Infra.Services.Entities.Student
{
    public class ServiceStudent : IServiceStudent
    {
        public Task<StudentModel> AddAsync(StudentModel entidades)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentModel>> AddListAsync(IEnumerable<StudentModel> entidades)
        {
            throw new NotImplementedException();
        }

        public void Remove(StudentModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
