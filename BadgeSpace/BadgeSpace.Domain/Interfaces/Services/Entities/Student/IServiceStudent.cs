using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Base;

namespace BadgeSpace.Domain.Interfaces.Services.Entities.Student
{
    public interface IServiceStudent : IServiceBase<StudentModel, int>
    {
        IQueryable<CertificationModel> ListCertifications(StudentModel entity);
    }
}
