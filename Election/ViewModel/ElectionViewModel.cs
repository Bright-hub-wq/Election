namespace Election.ViewModel
{
    public class ElectionViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<CandidateViewModel> Candidates { get; set; } = new List<CandidateViewModel>();
        public string Status { get; set; } // "Ongoing", "Ended", or "Upcoming"
    }
    //public string? Status { get; set; } // Ongoing, Ended, Pending
        //public List<CandidateViewModel>? Candidates { get; set; }
    
}
