using Election.Db;
using Election.IHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Services
{
    public class VoteCountingService : IVoteCountingService
    {
        private readonly AppDbContext _context;

        public VoteCountingService(AppDbContext context)
        {
            _context = context;
        }

        // Count votes for all ended elections that have not been processed
        public async Task CountVotesForEndedElections()
        {
            // Fetch elections with the "Ended" status that have not had their votes counted
            var endedElections = await _context.Elections
                .Include(e => e.Candidates)
                .Where(e => e.Status == "Ended" && e.VotesCounted == false)
                .ToListAsync();

            if (endedElections == null || !endedElections.Any()) return;

            foreach (var election in endedElections)
            {
                // Count votes for each candidate in the election
                foreach (var candidate in election.Candidates)
                {
                    candidate.Votes = await _context.Vote
                        .Where(v => v.CandidateId == candidate.Id && v.ElectionId == election.Id)
                        .CountAsync();
                }

                // Mark the election as "Votes Counted"
                election.VotesCounted = true;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        // Count votes for a specific election by ID
        public async Task CountVotesAsync(int electionId)
        {
            // Fetch the specific election with its candidates
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            if (election == null) return;

            // Count votes for each candidate
            foreach (var candidate in election.Candidates)
            {
                candidate.Votes = await _context.Vote
                    .Where(v => v.CandidateId == candidate.Id && v.ElectionId == election.Id)
                    .CountAsync();
            }

            // Update the election status to "Votes Counted"
            election.Status = "Votes Counted";

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
