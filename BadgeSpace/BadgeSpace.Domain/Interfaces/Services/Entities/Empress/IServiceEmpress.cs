using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Base;

namespace BadgeSpace.Domain.Interfaces.Services.Entities.Empress
{
    public interface IServiceEmpress : IServiceBase<EmpressModel, int>
    {
        IQueryable<CourseModel> ListCourses(EmpressModel entity);
    }
}
