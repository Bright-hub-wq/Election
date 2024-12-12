using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Election.Models
{
    public class Vote
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string? UserId { get; set; }
    //    [ForeignKey(nameof(UserId))]
    //    public ApplicationUser? User { get; set; }
    //    public int? BallotId { get; set; }
    //    [ForeignKey(nameof(BallotId))]
    //    public virtual Ballot? Ballot { get; set; }
    //    public int? CandidateId { get; set; }
    //    [ForeignKey(nameof(CandidateId))]
    //    public virtual Candidate? Candidate { get; set; }
    //    public DateTime? VoteDate { get; set; }

    //}
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
    }
}
