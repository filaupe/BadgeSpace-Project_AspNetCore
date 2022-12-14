using BadgeSpace.Domain.Resources.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    [Authorize(Roles = nameof(Roles.EMPRESA))]
    public class EmpressController : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public IActionResult Index() => View();
        
        public IActionResult Delete() => View();

        [AllowAnonymous]
        public IActionResult Details() => View();
        public IActionResult Edit() => View();
        
        public IActionResult Create() => View();

    }
}
