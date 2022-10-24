using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethods
    {
        public IQueryable<StudentModel> Get(DbSet<StudentModel> Students, int? id, params string[] types);
        public void Delete();
        public void Post();
        public void Put();
    }
}
