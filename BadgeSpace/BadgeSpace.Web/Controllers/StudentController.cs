using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Entities.User.Student;
using BadgeSpace.Domain.Interfaces.Repository.Student;
using BadgeSpace.Domain.Interfaces.Services.Entities.Student;
using BadgeSpace.Domain.Resources.Enums;
using BadgeSpace.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepositoryStudent _repostiroy;
        private readonly IServiceStudent _service;

        public StudentController(IRepositoryStudent context, IServiceStudent service)
        {
            _repostiroy = context;
            _service = service;
        }

        public StudentModel StudentUserCookie { get => Task.Run(async () => await _repostiroy.FirstOrDefaultAsync(u => u.Email.ToUpper() == User.Claims.ToList()[2].Value)).Result!; }

        [AcceptVerbs("GET", "POST")]
        [Authorize(Roles = nameof(Roles.ESTUDANTE))]
        public async Task<IActionResult> Index(string searchString = "", int skip = 0, int take = 8)
        {
            var badgesList = _service.ListCertifications(StudentUserCookie);
            if (!String.IsNullOrEmpty(searchString))
                badgesList = badgesList.Where(c => c.CourseName.ToLower().Contains(searchString.ToLower()));
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await badgesList.CountAsync() * 1M / take));
            return View(await badgesList.AsNoTracking().Skip(skip * take).Take(take).ToListAsync());
        }
    }
}
