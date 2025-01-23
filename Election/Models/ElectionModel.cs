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
        public ICollection<Candidate>? Candidates { get; set; }
        public bool VotesCounted { get; set; }
        public DateTime StartDate { get; set; }
        public bool? IsResultReleased { get; set; } = false;


        // This will represent the status based on the current date
        public bool IsReleased { get; set; }

        public int? WinnerId { get; set; }

        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        public string? WinnerName { get; set; }
        public int? WinnerVotes { get; set; }
        public string? WinnerParty { get; set; }
        public string? WinnerPhotoPath { get; set; }
        public ICollection<ElectionCandidate>? ElectionCandidates { get; set; }
        public int TotalVotes { get; set; } = 0;
        public bool ResultsReleased { get;  set; }
       // public string Name { get;  set; }

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

