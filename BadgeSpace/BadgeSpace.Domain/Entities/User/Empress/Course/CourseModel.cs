using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Entities.User.Student;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeSpace.Domain.Entities.User.Empress.Course
{
    public class CourseModel : EntityBaseModel
    {
        public CourseModel(byte[]? image, string courseName, string type, string level, string time, string description, int empressId)
        {
            Image = image;
            CourseName = courseName;
            Type = type;
            Level = level;
            Time = time;
            Description = description;
        }

        public byte[]? Image { get; set; } = null;
        public string CourseName { get; set; } = String.Empty;
        public string Identifier { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Level { get; set; } = String.Empty;
        public string Time { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        [ForeignKey(nameof(Empress))]
        public int EmpressId { get; set; }
        public EmpressModel Empress { get; set; } = null!;

        public List<StudentModel> Students { get; set; } = new();

        public static implicit operator CertificationModel(CourseModel model)
            => new()
            {
                Image = model.Image,
                CourseName = model.CourseName,
                Type = model.Type,
                Level = model.Level,
                Time = model.Time,
                Description = model.Description,
            };
    }
}
