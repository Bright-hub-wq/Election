

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Election.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        // User who cast the vote
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        // Candidate the user voted for
        [Required]
        public int? CandidateId { get; set; }
        [ForeignKey(nameof(CandidateId))]
        public virtual Candidate? Candidate { get; set; }

        // Timestamp of the vote
        [Required]
        public DateTime VoteDate { get; set; } = DateTime.Now;

        // Links the vote to a specific election
        [Required]
        public int ElectionId { get; set; }
        [ForeignKey(nameof(ElectionId))]
        public virtual ElectionModel? Election { get; set; }
        public DateTime VoteTime { get;  set; }
        public string? VoterId { get;  set; }
        public DateTime VotedAt { get;  set; }
    }
}

