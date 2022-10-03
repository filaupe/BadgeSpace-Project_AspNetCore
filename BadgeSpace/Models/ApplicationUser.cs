using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(14)]
        public string? CPF { get; set; }
        public bool Empresa { get; set; }
        public StudentModel student { get; set; }
    }
}
