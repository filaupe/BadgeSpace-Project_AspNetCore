using BadgeSpace.Domain.Resources.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.Web.Controllers
{
    public class StudentController : Controller
    {
        [Authorize(Roles = nameof(Roles.ESTUDANTE))]
        public IActionResult Index() => View(); //Dashboard
    }
}
