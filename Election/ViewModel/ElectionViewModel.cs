namespace Election.ViewModel
{
    public class ElectionViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool HasVoted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<CandidateViewModel> Candidates { get; set; }
        public string Status { get; set; } // "Ongoing", "Ended", or "Upcoming"
        public bool ResultsReleased { get; internal set; }
        public bool HasEnded { get; internal set; }
    }
    //public string? Status { get; set; } // Ongoing, Ended, Pending
       // public List<CandidateViewModel>? Candidates { get; set; }
    
}
