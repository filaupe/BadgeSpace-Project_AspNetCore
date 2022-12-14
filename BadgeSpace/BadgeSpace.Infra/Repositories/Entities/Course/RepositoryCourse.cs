using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Infra.Repositories.Entities.Base;

namespace BadgeSpace.Infra.Repositories.Entities.Course
{
    public class ReposiotoryCourse : RepositoryBase<CourseModel, int>, IRepositoryCourse
    {
        private readonly ApplicationDbContext _context;

        public ReposiotoryCourse(ApplicationDbContext context) : base(context) => _context = context;
    }
}
