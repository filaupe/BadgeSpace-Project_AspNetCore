using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethodDelete
    {
        public Task<object> Delete();
    }
}
