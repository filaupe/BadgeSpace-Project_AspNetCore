using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepositoryCourse _repository;
        private readonly IServiceCourse _service;

        public CourseController(IRepositoryCourse repository, IServiceCourse service)
        {
            _repository = repository;
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Index(int Id, string searchString = "", int skip = 0, int take = 8)
        {
            var course = await _repository.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null)
                return BadRequest();
            var studentsList = _service.ListStudents(course);
            if (!String.IsNullOrEmpty(searchString))
                studentsList = studentsList.Where(c => c.Email.ToLower().Contains(searchString.ToLower()));
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await studentsList.CountAsync() * 1M / take));
            return View(await studentsList.AsNoTracking().Skip(skip * take).Take(take).ToListAsync());
        }
    }
}
