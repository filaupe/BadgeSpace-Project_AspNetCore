using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Student;
using BadgeSpace.Infra.Repositories.Entities.Base;

namespace BadgeSpace.Infra.Repositories.Entities.Student
{
    public class ReposiotoryStudent : RepositoryBase<StudentModel, int>, IRepositoryStudent
    {
        private readonly ApplicationDbContext _context;

        public ReposiotoryStudent(ApplicationDbContext context) : base(context) => _context = context;
    }
}
