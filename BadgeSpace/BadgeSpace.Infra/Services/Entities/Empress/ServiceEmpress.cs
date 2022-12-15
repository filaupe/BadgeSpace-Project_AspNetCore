using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Empress;

namespace BadgeSpace.Infra.Services.Entities.Empress
{
    public class ServiceEmpress : IServiceEmpress
    {
        private readonly IRepositoryCourse _repositoryCourse;

        public ServiceEmpress(IRepositoryCourse repositoryCourse)
        {
            _repositoryCourse = repositoryCourse;
        }

        public Task<EmpressModel> AddAsync(EmpressModel entidades)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmpressModel>> AddListAsync(IEnumerable<EmpressModel> entidades)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CourseModel> ListCourses(EmpressModel entity)
            => _repositoryCourse.Where(c => c.EmpressId == entity.Id);

        public void Remove(EmpressModel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
