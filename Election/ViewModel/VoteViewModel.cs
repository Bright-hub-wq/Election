namespace Election.ViewModel
{
    public class VoteViewModel
    {
        public int ElectionId { get; set; }
        public string ElectionTitle { get; set; }
        public string ElectionDescription { get; set; }
        public List<CandidateViewModel> Candidates { get; set; }

        public VoteViewModel()
        {
            Candidates = new List<CandidateViewModel>();
        }
    }

}
