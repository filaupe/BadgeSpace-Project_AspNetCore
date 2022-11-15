using Domain_Driven_Design.Domain.Recursos.Enums;
using Domain_Driven_Design.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index() => View();

        [Authorize(Roles = nameof(Roles.USUARIO))]
        public IActionResult Dashboard() => View(_context.Estudantes.Where(s => s.CPF == User.Claims.ToList()[2].Value));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}