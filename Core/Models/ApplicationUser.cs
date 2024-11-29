using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		[NotMapped]
		public string Name => FirstName + " " + LastName;
		public bool IsDeactivated { get; set; }
		public DateTime? DateRegistered { get; set; }
		[NotMapped]
		public string? Role { get; set; }
		public DateTime? DateOfBirth { get; set; }
	}
}
