using Election.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Candidate
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Party { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Position { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public string? PhotoPath { get; set; }

    public int Votes { get; set; } = 0;

    public bool IsInElection { get; set; } = false;
   // public int VoteCount { get; set; } // Optional, depends on whether you calculate votes dynamically
    //public ICollection<Vote>? Votes { get; set; }
    public int? ElectionId { get; set; }

    [ForeignKey(nameof(ElectionId))]
    public ElectionModel? Election { get; set; }

    public int? BallotId { get; set; }
}
