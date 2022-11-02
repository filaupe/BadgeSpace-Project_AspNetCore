using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethodGet
    {
        public Task<StudentModel?> GetById(ApplicationDbContext _context, int id);
        public IQueryable<StudentModel> Get(DbSet<StudentModel> Students, params string[] types);
    }
}
