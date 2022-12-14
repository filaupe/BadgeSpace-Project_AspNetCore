using BadgeSpace.Domain.Interfaces.Services.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.Infra.Resources.Validation
{
    public class ValidationMethods : Controller
    {
        private readonly IServiceUser _serviceUser;

        public ValidationMethods(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        public async Task<IActionResult> VerifyEmailAdress(string Email)
            => await _serviceUser.VerifyEmailAdress(Email)
                ? Json("O Email já está em uso.") : Json(true);

        public IActionResult VerifyPassword(string Password, string CurrentPassword)
            => Password != CurrentPassword ? Json($"Está senha não é a sua atual") : Json(true);

        public IActionResult VerifyCode(string Code) => Json(true); //pega no mkLeads
    }
}
