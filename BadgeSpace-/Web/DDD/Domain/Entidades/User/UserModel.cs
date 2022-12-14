using Domain_Driven_Design.Domain.Entidades.Base;
using Web.DDD.Domain.Entidades.User.Certification.Empress;
using Web.DDD.Domain.Entidades.User.Certification.Student;

namespace Domain_Driven_Design.Domain.Entidades.Usuario
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
