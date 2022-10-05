using BadgeSpace.Data;
using BadgeSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BadgeSpace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            var identidy = "";
            var empresa = false;
            foreach (var item in _context.Users)
            {
                if (item.Email == User.Identity.Name)
                {
                    identidy = item.CPF;
                    empresa = item.Empresa;
                }
            }
            ViewBag.CPF = identidy;
            if (empresa)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_context.Students.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}