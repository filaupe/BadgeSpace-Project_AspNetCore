using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Password() => View();

        public IActionResult Email() => View();

        public IActionResult ApiKey() => View();
    }
}
