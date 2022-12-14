using BadgeSpace.Web.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BadgeSpace.Web.Models.User
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        [DisplayName("Email")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
    }
}
