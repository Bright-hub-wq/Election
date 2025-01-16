

namespace Election.Models
{
    public class Ballot
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? BallotStartDate { get; set; }
        public DateTime? BallotEndDate { get; set; }
        public ICollection<Candidate>? Candidates { get; set; }

    }

}

