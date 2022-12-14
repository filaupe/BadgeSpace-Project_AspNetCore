using BadgeSpace.Domain.Entities.User;
using BadgeSpace.Domain.Interfaces.Repository.User;
using BadgeSpace.Domain.Interfaces.Services.Autentication;
using BadgeSpace.Domain.Interfaces.Services.Entities.User;
using BadgeSpace.Domain.Resources.Enums;
using BadgeSpace.Infra;
using BadgeSpace.Infra.Services.Authentication;
using BadgeSpace.Web.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IServiceUser _serviceUser;
        private readonly IAuthCookieService _authCookieService;
        private readonly IAuthJsonWebTokenService _authJwtService;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryUser _repository;

        public AuthenticationController(IServiceUser serviceUser, ApplicationDbContext context,
            IRepositoryUser repository, IAuthCookieService authCookieService, IAuthJsonWebTokenService authJwtService)
        {
            _serviceUser = serviceUser;
            _authCookieService = authCookieService;
            _authJwtService = authJwtService;
            _context = context;
            _repository = repository;
        }

        public IActionResult Login()
            => User.Identity!.IsAuthenticated
                ? RedirectToAction("Index", "Home") : View();

        public IActionResult Register()
            => User.Identity!.IsAuthenticated
                ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Token = await _authJwtService.GenerateToken((UserModel)model);
            await _repository.AddAsync((UserModel)model);
            try
            {
                await _context.SaveChangesAsync();
            } finally { }
            await Login((UserLoginViewModel)model);

            if (User.IsInRole(nameof(Roles.EMPRESA)))
                return RedirectToAction("Index", "Empress");
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (await _repository.AnyAsync(u => u.NormalizedEmail == model.Email.ToUpper() && u.Password == model.Password))
            {
                var user = await _repository.FirstOrDefaultAsync(u => u.NormalizedEmail == model.Email.ToUpper() && u.Password == model.Password);
                await _authCookieService.GenerateCookies(user!, HttpContext);

                if (User.IsInRole(nameof(Roles.EMPRESA)))
                    return RedirectToAction("Index", "Empress");
                return RedirectToAction("Index", "Student");
            }
            return View(model);
        }
    }
}
