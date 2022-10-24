using Web.Data;
using Web.Models;
using Web.Models.Enums;
using Web.Utils.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Utils.MethodsExtensions.AddMethods.Interfaces;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMethods _Method;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMethods Method)
        {
            _logger = logger;
            _context = context;
            _Method = Method;
        }

        public IActionResult Index() => View();

        public IActionResult API() => View();

        public IActionResult Sobre() => View();

        [Authorize(Roles = nameof(Roles.STUDENT))]
        public IActionResult Dashboard()
            => View(_Method.Get(_context.Students, null, LinkData(User.Identity!.Name!.ToString()).Result!.CPF_CNPJ));


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            =>  View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        #region Private Methods
        private async Task<ApplicationUser?> LinkData(string identity) 
            => await _context.Users.FirstOrDefaultAsync(c => c.Email == identity);
        #endregion
    }
}