using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Student;

namespace BadgeSpace.Domain.Entities.User
{
    public class UserModel : EntityBaseModel
    {
        public UserModel(string? name, string email, string password, bool claim = false) //falta o token
        {
            Name = String.IsNullOrWhiteSpace(name) ? email[0..email.IndexOf("@")] : name;
            Email = email;
            NormalizedEmail = email.ToUpper();
            Password = password;
            Claim = claim;
            if (claim)
                Empress = new();
            Student = new();
        }

        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string NormalizedEmail { get; set; } = String.Empty;
        public byte[]? Image { get; set; } = null;
        public string Password { get; set; } = String.Empty;
        public bool Claim { get; set; } = false;
        public string Token { get; set; } = String.Empty;

        public StudentModel? Student { get; set; } = null;
        public EmpressModel? Empress { get; set; } = null;
    }
}
