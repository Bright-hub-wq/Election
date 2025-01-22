using Election.ViewModel;

namespace Election.Models
{
    public class ElectionVoteViewModel
    {
        public int ElectionId { get; set; }
        public string? Title { get; set; }
        public string? ElectionTitle { get; set; } // Add this
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // public IEnumerable<CandidateViewModel> Candidates { get; set; }
        //public List<CandidateViewModel> Candidates { get; set; }
        public IEnumerable<Candidate> Candidates { get; set; }
        public bool HasVoted { get; set; } // Add this
        public int? VotedCandidateId { get; set; }

    }

}
