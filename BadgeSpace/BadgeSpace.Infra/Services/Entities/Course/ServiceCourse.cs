using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;

namespace BadgeSpace.Infra.Services.Entities.Course
{
    public class ServiceCourse : IServiceCourse
    {
        private readonly IRepositoryCourse _repository;

        public ServiceCourse(IRepositoryCourse repository)
        {
            _repository = repository;
        }

        public async Task<CourseModel> AddAsync(CourseModel entity)
        {
            return await _repository.AddAsync(entity);
        }

        public Task<IEnumerable<CourseModel>> AddListAsync(IEnumerable<CourseModel> entidades)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentModel> ListStudents(CourseModel entity)
            => throw new NotImplementedException();

        public void Remove(CourseModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
