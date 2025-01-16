namespace Election.Models
{
    public class ElectionVoteViewModel
    {
        public int ElectionId { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Candidate> Candidates { get; set; }
    }

}
