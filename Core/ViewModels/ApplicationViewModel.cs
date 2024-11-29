using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ApplicationViewModel
    {

        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool isdeactivated { get; set; }
        public DateTime? DateRegistered { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match. ")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
