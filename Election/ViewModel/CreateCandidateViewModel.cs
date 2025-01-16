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

        [MaxLength(500)]
        public string? Description { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; } // Path to store in the database
    }
}


