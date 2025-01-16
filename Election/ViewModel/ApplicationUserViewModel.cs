using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Election.ViewModel;

public class ApplicationUserViewModel
{
    public string? FirstName { get; set; }
    public string? UserId { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
    public string? Password { get; set; }
    public bool isdeactivated { get; set; }
    public DateTime? DateRegistered { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Name => FirstName + " " + LastName; 
    [NotMapped]
    public string? Role { get; set; }
  

}
