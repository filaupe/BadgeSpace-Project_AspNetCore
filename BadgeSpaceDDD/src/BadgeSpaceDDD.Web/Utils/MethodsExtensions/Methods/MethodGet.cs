using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.Methods
{
    public class MethodGet
    {

        public async Task<IQueryable<StudentModel>> Listar(DbSet<StudentModel> Students, int? id, params string[] types)
        {
            var (student, students) = (Students.Where(c => c.Id != null), Students);
            foreach (var parameter in types)
                student = parameter != null ? students
                    .Where(c => c.EmpresaId == Convert.ToString(parameter) || c.AlunoCPF == Convert.ToString(parameter)) : student;
            var stdt = Students.Where(c => c.Id == id);
            return id.HasValue && id != 0 ? stdt : student;
        }
    }
}
