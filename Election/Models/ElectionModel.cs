using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Election.Models
{
    public class ElectionModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }
        //public int ElectionId { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public ICollection<Candidate> Candidates { get; set; }

        public DateTime StartDate { get; set; }
        public bool? IsResultsReleased { get; set; } = false;

        // This will represent the status based on the current date

        public int? WinnerId { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<ElectionCandidate> ElectionCandidates { get; set; } = new List<ElectionCandidate>();

        public bool IsOngoing()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate >= StartDate && currentDate <= EndDate;
        }




        public bool HasEnded()
        {
            return DateTime.Now > EndDate;
        }
    }
}

