using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethodGet
    {
        public IQueryable<StudentModel> Get(DbSet<StudentModel> Students, int? id, params string[] types);
    }
}
