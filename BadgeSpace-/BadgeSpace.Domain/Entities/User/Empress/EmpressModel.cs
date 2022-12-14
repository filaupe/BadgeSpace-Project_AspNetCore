using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeSpace.Domain.Entities.User.Empress
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
