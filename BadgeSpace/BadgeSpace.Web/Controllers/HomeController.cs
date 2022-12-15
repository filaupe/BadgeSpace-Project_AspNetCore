using BadgeSpace.Web.Models;
using BadgeSpace.Web.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BadgeSpace.Web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] public IActionResult Index() => View();

        [AllowAnonymous] public IActionResult API() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}