using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Repository.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;

namespace BadgeSpace.Infra.Services.Entities.Course
{
    public class ServiceCourse : IServiceCourse
    {
        private readonly IRepositoryCourse _repositoryCourse;
        private readonly IRepositoryStudent _repositoryStudent;

        public ServiceCourse(IRepositoryCourse repositoryCourse, IRepositoryStudent repositoryStudent)
        {
            _repositoryCourse = repositoryCourse;
            _repositoryStudent = repositoryStudent;
        }

        public async Task<CourseModel> AddAsync(CourseModel entity)
        {
            return await _repositoryCourse.AddAsync(entity);
        }

        public Task<IEnumerable<CourseModel>> AddListAsync(IEnumerable<CourseModel> entidades)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentModel> ListStudents(CourseModel entity)
            => _repositoryStudent.Where(c => c.CourseId == entity.Id);

        public void Remove(CourseModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
