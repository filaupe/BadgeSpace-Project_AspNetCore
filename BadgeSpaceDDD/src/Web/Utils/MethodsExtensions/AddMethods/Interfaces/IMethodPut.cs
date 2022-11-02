using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethodPut
    {
        public Task<object> Put();
    }
}
