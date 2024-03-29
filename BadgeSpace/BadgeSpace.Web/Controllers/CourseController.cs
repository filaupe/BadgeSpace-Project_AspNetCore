﻿using BadgeSpace.Domain.Entities.Certification;
using BadgeSpace.Domain.Entities.User.Empress;
using BadgeSpace.Domain.Interfaces.Repository.Course;
using BadgeSpace.Domain.Interfaces.Repository.Empress;
using BadgeSpace.Domain.Interfaces.Services.Entities.Course;
using BadgeSpace.Infra;
using BadgeSpace.Infra.Repositories.Entities.Empress;
using BadgeSpace.Web.Models.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryCourse _repositoryCourse;
        private readonly IRepositoryEmpress _repositoryEmpress;
        private readonly IServiceCourse _service;

        public CourseController(IRepositoryCourse repositoryCourse, IRepositoryEmpress repositoryEmpress, 
            IServiceCourse service, ApplicationDbContext context)
        {
            _context = context;
            _repositoryEmpress = repositoryEmpress;
            _repositoryCourse = repositoryCourse;
            _service = service;
        }

        public EmpressModel EmpressUserCookie { get => Task.Run(async () => await _repositoryEmpress.FirstOrDefaultAsync(u => u.Email.ToUpper() == User.Claims.ToList()[2].Value)).Result!; }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Index(int Id, string searchString = "", int skip = 0, int take = 8)
        {
            if (!await _repositoryCourse.Where(c => c.EmpressId == EmpressUserCookie.Id).AnyAsync(c => c.Id == Id))
                return Unauthorized();
            var course = await _repositoryCourse.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null)
                return BadRequest();
            var studentsList = _service.ListStudents(course);
            if (!String.IsNullOrEmpty(searchString))
                studentsList = studentsList.Where(c => c.Email.ToLower().Contains(searchString.ToLower()));
            ViewBag.Pages = Convert.ToInt32(Math.Ceiling(await studentsList.CountAsync() * 1M / take));
            return View(await studentsList.AsNoTracking().Skip(skip * take).Take(take).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            StudentViewModel student = new();
            var courseList = await _repositoryCourse.Where(c => c.EmpressId == EmpressUserCookie.Id).ToListAsync();
            courseList.ForEach(c => student.Courses.Add(new SelectListItem { Text = c.CourseName, Value = c.Id.ToString() }));
            return View(student);
        }

        public async Task<IActionResult> OnCreate(StudentViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));
            var course = await _context.Courses.FindAsync(Convert.ToInt32(model.CourseId));
            CertificationModel certification = course!;
            certification.StudentEmail = model.Email;
            certification.StudentName = model.Name;
            certification.EmpressEmail = EmpressUserCookie.Email;
            await _context.Certifications.AddAsync(certification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
