namespace Election.IHelper
{
    public interface IVoteCountingService
    {
        Task CountVotesAsync(int electionId); // For counting votes for a specific election
    }
}

