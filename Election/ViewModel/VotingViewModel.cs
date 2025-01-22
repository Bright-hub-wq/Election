namespace Election.ViewModel
{
    public class VotingViewModel
    {
        public int ElectionId { get; set; }
        public string? ElectionTitle { get; set; }
        public string? ElectionDescription { get; set; }
        public List<CandidateViewModel>? Candidates { get; set; }
    }

}
