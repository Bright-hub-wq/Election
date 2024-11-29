using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Party { get; set; }
        public int BallotId { get; set; }
        [ForeignKey(nameof(BallotId))]
        public virtual Ballot? Ballot { get; set; }
    }

}
