using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Certification;
using BadgeSpace.Domain.Interfaces.Repository.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Student;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Infra.Services.Entities.Student
{
    public class ServiceStudent : IServiceStudent
    {
        private readonly IRepositoryStudent _repositoryStudent;
        private readonly IRepositoryCertification _repositoryCertification;

        public ServiceStudent(IRepositoryStudent repositoryStudent, IRepositoryCertification repositoryCertification)
        {
            _repositoryStudent = repositoryStudent;
            _repositoryCertification = repositoryCertification;
        }

        public Task<StudentModel> AddAsync(StudentModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentModel>> AddListAsync(IEnumerable<StudentModel> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CertificationModel> ListCertifications(StudentModel entity)
            => _repositoryCertification.Where(c => c.StudentEmail.ToUpper() == entity.Email.ToUpper());

        public void Remove(StudentModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
