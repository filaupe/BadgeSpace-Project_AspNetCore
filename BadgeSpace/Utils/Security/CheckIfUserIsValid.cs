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

            // Caso ache algo, o retorno será (true, valorEncontrado)
            // Caso não encontre, o retorno será (false, null)
            return (validUser != null, validUser);
        }

        public static async Task<bool> IsUserValid(DbSet<ApplicationUser> applicationUsers, string CpfCnpj)
            => await applicationUsers.AnyAsync(u => u.CPF_CNPJ == CpfCnpj);
    }
}
