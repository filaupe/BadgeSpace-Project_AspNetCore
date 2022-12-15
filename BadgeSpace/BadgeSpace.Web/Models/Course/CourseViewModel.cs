using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Web.Models.Base;

namespace BadgeSpace.Web.Models.Course
{
    public class CourseViewModel : BaseViewModel
    {
        public int EmpressId { get; set; }

        public byte[]? Image { get; set; } = null;
        public string CourseName { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Level { get; set; } = String.Empty;
        public string Time { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public static implicit operator CourseModel(CourseViewModel model)
            => new(model.Image, model.CourseName, model.Type, 
                model.Level, model.Time, model.Description, model.EmpressId);
    }
}
