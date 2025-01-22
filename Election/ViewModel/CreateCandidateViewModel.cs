using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Election.Models
{
    public class CreateCandidateViewModel
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Party { get; set; }

        [StringLength(100)]
        public string? Position { get; set; }
        public int ElectionId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public List<SelectListItem>? AvailableElections { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; } // Path to store in the database
    }
}


