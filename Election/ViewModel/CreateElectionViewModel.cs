using Election.Models.Election.Models;
using System.ComponentModel.DataAnnotations;

namespace Election.Models
{
    public class CreateElectionViewModel
    {
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        // List of selected candidate IDs from the form
        public List<int>? SelectedCandidates { get; set; }
        public List<int>? SelectedCandidateIds { get; set; } // IDs of selected candidates
        public List<Candidate>? Candidates { get; set; } // List of candidates that will be shown to the admin


        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public List<CandidateModel>? AvailableCandidates { get; set; }

        // List of available candidates to assign to this election
    }

}
