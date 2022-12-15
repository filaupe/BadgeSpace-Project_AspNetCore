using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Base;

namespace BadgeSpace.Domain.Interfaces.Services.Entities.Course
{
    public interface IServiceCourse : IServiceBase<CourseModel, int>
    {
        IQueryable<StudentModel> ListStudents(CourseModel entity);
    }
}
