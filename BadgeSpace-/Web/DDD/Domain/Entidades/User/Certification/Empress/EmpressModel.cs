using Domain_Driven_Design.Domain.Entidades.Base;
using Domain_Driven_Design.Domain.Entidades.Usuario;
using System.ComponentModel.DataAnnotations.Schema;
using Web.DDD.Domain.Entidades.User.Certification.Empress.Course;

namespace Web.DDD.Domain.Entidades.User.Certification.Empress
{
    public class EmpressModel : EntityBaseModel
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        public List<CourseModel> Courses { get; set; } = new();
    }
}
