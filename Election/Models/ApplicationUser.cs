using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Election.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public bool IsDeactivated { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public DateTime? DateRegistered { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [NotMapped]
        public string Name => FirstName + " " + LastName;
        [NotMapped]
        public string? Role { get; set; }
    }
}