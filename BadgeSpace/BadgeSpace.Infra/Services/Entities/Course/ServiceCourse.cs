using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;

namespace BadgeSpace.Infra.Services.Entities.Course
{
    public class ServiceCourse : IServiceCourse
    {
        public Task<CourseModel> AddAsync(CourseModel entidades)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseModel>> AddListAsync(IEnumerable<CourseModel> entidades)
        {
            throw new NotImplementedException();
        }

        public void Remove(CourseModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
