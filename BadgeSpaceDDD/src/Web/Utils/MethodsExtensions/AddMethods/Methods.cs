using Web.Utils.MethodsExtensions.AddMethods.Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;
using System.Security.Principal;

namespace Web.Utils.MethodsExtensions.AddMethods
{
    public class Methods : IMethods
    {
        public object getMessage(string message) => new { message = message };

        public async Task<StudentModel?> GetById(ApplicationDbContext _context, int id) => await _context.Students.FirstOrDefaultAsync(c => c.Id == id);

        public IQueryable<StudentModel> Get(DbSet<StudentModel> Students, params string[] types)
        {
            IQueryable<StudentModel> students = Students.Where(c => c.EmpresaId != null);
            foreach (var parameter in types)
                students = parameter != null ? students.Where(c => c.EmpresaId == parameter || c.AlunoCPF == parameter) : students;
            return students;
        }

        public async Task<object> Put()
        {
            return getMessage("");
        }

        public async Task<object> Post(StudentModel Student, ApplicationDbContext _context, string CPF, string empresa)
        {
            Student.AlunoCPF = CPF;
            Student.EmpresaId = empresa;

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();
            return getMessage("aaaa");
        }

        public async Task<object> Delete()
        {
            return getMessage("");
        }
    }
}
