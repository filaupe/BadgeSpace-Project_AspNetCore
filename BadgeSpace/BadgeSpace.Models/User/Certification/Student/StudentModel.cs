using BadgeSpace.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeSpace.Models.User.Certification.Student
{
    public class StudentModel : EntityBaseModel
    {
        public StudentModel(UserModel user)
        {
            UserId = user.Id;
            User = user;
            Name = user.Name;
            Email = user.Email;
        }

        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        public List<CertificationModel> Certifications { get; set; } = new();
    }
}
