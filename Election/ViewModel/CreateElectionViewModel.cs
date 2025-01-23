using Election.Models.Election.Models;
using Election.ViewModel;
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
        //public List<int>? SelectedCandidateIds { get; set; } // IDs of selected candidates
        public List<Candidate>? Candidates { get; set; } // List of candidates that will be shown to the admin


        // List of available candidates to display in the form
        //public List<CandidateViewModel>? AvailableCandidates { get; set; }


        public List<CandidateViewModel>? AvailableCandidates { get; set; }
        // IDs of selected candidates
        public List<int>? SelectedCandidateIds { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


    }

}
