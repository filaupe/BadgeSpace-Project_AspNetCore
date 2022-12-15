using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Repository.Empress;
using BadgeSpace.Domain.Interfaces.Services.Entities.Empress;
using BadgeSpace.Domain.Resources.Enums;
using BadgeSpace.Infra;
using BadgeSpace.Web.Models.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    [Authorize(Roles = nameof(Roles.EMPRESA))]
    public class EmpressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryEmpress _repostiroyEmpress;
        private readonly IRepositoryCourse _repostiroyCourse;
        private readonly IServiceEmpress _service;

        public EmpressController(IRepositoryEmpress repostiroyEmpress, IRepositoryCourse repositoryCourse,
            IServiceEmpress service, ApplicationDbContext context)
        {
            _repostiroyEmpress = repostiroyEmpress;
            _repostiroyCourse = repositoryCourse;   
            _service = service;
            _context = context;
        }

        public EmpressModel EmpressUserCookie { get => Task.Run(async () => await _repostiroyEmpress.FirstOrDefaultAsync(u => u.Email.ToUpper() == User.Claims.ToList()[2].Value)).Result!; }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Index(string searchString = "", int skip = 0, int take = 8)
        {
            if (EmpressUserCookie == null)
                return BadRequest();
            var coursesList = _service.ListCourses(EmpressUserCookie);
            if (!String.IsNullOrEmpty(searchString))
                coursesList = coursesList.Where(c => c.CourseName.ToLower().Contains(searchString.ToLower()));
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await coursesList.CountAsync() * 1M / take));
            return View(await coursesList.AsNoTracking().Skip(skip * take).Take(take).ToListAsync());
        }

        public IActionResult Create() => View();

        public IActionResult Students(int Id) => RedirectToAction("Index", "Course", Id);

        public async Task<IActionResult> Delete(int Id)
        {
            var course = await _repostiroyCourse.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null)
                return BadRequest();
            if (course.EmpressId != EmpressUserCookie.Id)
                return Unauthorized();
            return View(course);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var course = await _repostiroyCourse.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null)
                return BadRequest();
            if (course.EmpressId != EmpressUserCookie.Id)
                return Unauthorized();
            return View(course);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var course = await _repostiroyCourse.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null)
                return BadRequest();
            if (course.EmpressId != EmpressUserCookie.Id)
                return Unauthorized();
            return View(course);
        }

        public async Task<IActionResult> OnCreate(CourseViewModel model)
        {
            model.EmpressId = EmpressUserCookie.Id;
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));
            await _repostiroyCourse.AddAsync((CourseModel)model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
