using Microsoft.AspNetCore.Identity;

namespace BadgeSpace.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CPF { get; set; }
    }
}
