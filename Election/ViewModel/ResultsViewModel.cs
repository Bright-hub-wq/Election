namespace Election.ViewModel
{
    public class ResultsViewModel
    {
        public int ElectionId { get; set; }
        public string ElectionTitle { get; set; }
        public string ElectionDescription { get; set; }
        public CandidateViewModel Winner { get; set; }
        public List<CandidateViewModel> Candidates { get; set; }
    }

}
