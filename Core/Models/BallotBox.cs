using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BallotBox
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public DateTime? BallotStartDate { get; set; }
        public DateTime? BallotEndDate { get; set; }
        public ICollection<Candidate>? Candidates { get; set; }

    }

}
