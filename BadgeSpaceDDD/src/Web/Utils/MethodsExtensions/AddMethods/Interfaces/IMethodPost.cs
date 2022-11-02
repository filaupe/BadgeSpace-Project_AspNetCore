using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethodPost
    {
        public Task<object> Post(StudentModel Student, ApplicationDbContext _context, string CPF, string empresa);
    }
}
