namespace Election.ViewModel
{
    
        public class CandidateViewModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? Party { get; set; }
            public int CandidateId { get; set; }
            public string? PhotoPath { get; set; }
        public int VoteCount { get; set; }
        public bool IsWinner { get; set; }
    }
    

}
