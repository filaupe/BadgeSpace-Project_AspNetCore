using BadgeSpace.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BadgeSpace.Utils.Security
{
    public static class CheckIfUserIsValid
    {
        public static (bool, ApplicationUser?) IsUserValid(DbSet<ApplicationUser> applicationUsers, IPrincipal identity)
        {
            var validUser = applicationUsers.FirstOrDefault(u => u.Email == identity.Identity!.Name);
            return (validUser != null, validUser);
        }

        public static async Task<bool> IsUserValid(DbSet<ApplicationUser> applicationUsers, string CpfCnpj)
            => await applicationUsers.AnyAsync(u => u.CPF_CNPJ == CpfCnpj);
    }
}
