using BadgeSpace.Models.Base;
using BadgeSpace.Models.User.Certification.Empress.Course;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeSpace.Models.User.Certification.Empress
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
