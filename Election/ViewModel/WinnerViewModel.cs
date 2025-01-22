

namespace Election.ViewModel
{
    public class WinnerViewModel
    {
        public string? Name { get; set; }
        public string? Party { get; set; }
        public string? Photopath { get; set; }
        public int VoteCount { get; set; }
        public string? ElectionTitle { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? WinnerName { get; set; }
        public string? WinnerPhotoPath { get; set; }
        public string? WinnerParty { get; set; }
        public int WinnerVoteCount { get; set; }
    }
}
