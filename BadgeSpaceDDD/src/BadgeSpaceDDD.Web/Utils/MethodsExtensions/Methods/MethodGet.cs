using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.Methods
{
    public class MethodGet
    {

        public async Task<IQueryable<StudentModel>> Listar(DbSet<StudentModel> Students, int? id, params string[] types)
        {
            IQueryable<StudentModel> students = Students.Where(c => c.EmpresaId != null);
            foreach (var parameter in types)
                students = parameter != null ? students
                    .Where(c => c.EmpresaId == parameter || c.AlunoCPF == parameter) : students;
            var stdt = Students.Where(c => c.Id == id);
            return id.HasValue && id != 0 ? stdt : students;
        }
    }
}
