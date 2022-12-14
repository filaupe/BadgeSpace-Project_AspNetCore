using Domain_Driven_Design.Domain.Entidades.Base;
using Domain_Driven_Design.Domain.Entidades.Usuario;
using System.ComponentModel.DataAnnotations.Schema;
using Web.DDD.Domain.Entidades.User.Certification.Empress.Course;

namespace Web.DDD.Domain.Entidades.User.Certification.Student
{
    public class StudentModel : EntityBaseModel
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; } = null;
        public CourseModel? Course { get; set; } = null;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        public List<CertificationModel> Certifications { get; set; } = new();
    }
}
