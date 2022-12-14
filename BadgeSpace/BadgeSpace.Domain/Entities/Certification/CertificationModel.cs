using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Entities.User.Student;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeSpace.Domain.Entities.Certification
{
    public class CertificationModel : EntityBaseModel
    {
        public byte[]? Image { get; set; } = null;
        public string Code { get; set; } = String.Empty;
        public string CourseName { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Level { get; set; } = String.Empty;
        public string Time { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public string StudentName { get; set; } = String.Empty;
        public string StudentEmail { get; set; } = String.Empty;
        public string EmpressEmail { get; set; } = String.Empty;

        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; } = null;

        [ForeignKey(nameof(Student))]
        public int? StudentId { get; set; } = null;

        public CourseModel? Course { get; set; } = null;
        public StudentModel? Student { get; set; } = null;
    }
}