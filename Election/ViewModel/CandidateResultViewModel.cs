namespace Election.ViewModel
{
    public class CandidateResultViewModel
    {
        public string Name { get; set; }
        public int TotalVotes { get; set; }
        public int CandidateId { get;  set; }
        public string? Photopath { get;  set; }
        public int VoteCount { get;  set; }
        public string? Party { get;  set; }
    }
}
