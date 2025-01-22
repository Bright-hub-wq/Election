namespace Election.ViewModel
{
    public class ElectionResultViewModel
    {
        public string ElectionTitle { get; set; }
        public List<CandidateResultViewModel> Candidates { get; set; }
    }
}
