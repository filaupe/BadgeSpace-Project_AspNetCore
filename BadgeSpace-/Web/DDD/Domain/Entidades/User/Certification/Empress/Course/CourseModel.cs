using Domain_Driven_Design.Domain.Entidades.Base;
using System.ComponentModel.DataAnnotations.Schema;
using Web.DDD.Domain.Entidades.User.Certification.Student;

namespace Web.DDD.Domain.Entidades.User.Certification.Empress.Course
{
    public class CourseModel : EntityBaseModel
    {
        public byte[]? Image { get; set; } = null;
        public string Code { get; set; } = String.Empty;
        public string CourseName { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Level { get; set; } = String.Empty;
        public string Time { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        [ForeignKey(nameof(Empress))]
        public int EmpressId { get; set; }
        public EmpressModel Empress { get; set; } = null!;

        public List<StudentModel> Students { get; set; } = new();
    }
}
