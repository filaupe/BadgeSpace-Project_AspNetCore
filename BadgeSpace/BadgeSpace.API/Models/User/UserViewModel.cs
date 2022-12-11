using BadgeSpace.API.Models.Base;
using BadgeSpace.Models.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.API.Models
{
    public class UserViewModel : BaseViewModel
    {
        public string Name { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = String.Empty;

        public bool Claim { get; set; } = false;

        public static implicit operator UserModel(UserViewModel model)
            => new(model.Name, model.Email, model.Password, model.Claim);
    }
}
