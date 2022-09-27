using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CPF { get; set; }
    }
}
