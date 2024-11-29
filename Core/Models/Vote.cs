using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        public int BallotId { get; set; }
        [ForeignKey(nameof(BallotId))]
        public  virtual Ballot? Ballot { get; set; }
        public int CandidateId { get; set; }
        [ForeignKey(nameof(CandidateId))]
        public virtual Candidate? Candidate { get; set; }
        public DateTime VoteDate { get; set; }
    }

}
