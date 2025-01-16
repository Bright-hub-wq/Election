using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Election.Models
{
    public class ElectionCandidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ElectionId { get; set; }

        [ForeignKey(nameof(ElectionId))]
        public virtual ElectionModel? Election { get; set; }
        public int Votes { get; set; } // To store the vote count for each candidate


        [Required]
        public int CandidateId { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public virtual Candidate? Candidate { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
