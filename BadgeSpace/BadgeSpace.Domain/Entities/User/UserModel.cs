using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Student;

namespace BadgeSpace.Domain.Entities.User
{
    public class UserModel : EntityBaseModel
    {
        public UserModel(string? name, string email, string password, string token, bool claim = false)
        {
            Name = String.IsNullOrWhiteSpace(name) ? email[0..email.IndexOf("@")] : name;
            Email = email;
            NormalizedEmail = email.ToUpper();
            Password = password;
            Claim = claim;
            Token = token;
            if (claim)
            {
                Empress = new() { Name = this.Name, Email = this.Email };
            }
            else
            {
                Student = new() { Name = this.Name, Email = this.Email };
            }
           
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
