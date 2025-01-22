namespace Election.ViewModel
{
    public class VoteNowViewModel
    {
        public int ElectionId { get; set; }
        public string? ElectionTitle { get; set; }
        public List<CandidateViewModel>? Candidates { get; set; }
    }

}
