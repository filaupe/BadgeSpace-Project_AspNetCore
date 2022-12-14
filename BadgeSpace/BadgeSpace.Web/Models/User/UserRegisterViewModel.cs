 using BadgeSpace.Web.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
namespace BadgeSpace.Web.Models.User
{
    public class UserRegisterViewModel : BaseViewModel
    {
        [DisplayName("Nome de Usuário")]
        public string? Name { get; set; } = null;

        [Remote(action: "VerifyEmailAdress", controller: "ValidationMethods")]
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Estudante / Empresa")]
        public bool Claim { get; set; } = false;

        [Required(ErrorMessage = "A área Senha é obrigatória")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "A área deve haver ao menos 8 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "A área Confirmar Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare(nameof(Password), ErrorMessage = "As senhas não são iguais")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public static implicit operator UserLoginViewModel(UserRegisterViewModel model)
            => new()
            {
                Email = model.Email,
                Password = model.Password,
            };
        public static implicit operator UserModel(UserRegisterViewModel model)
            => new(model.Name, model.Email, model.Password, model.Token, model.Claim);
    }
}
