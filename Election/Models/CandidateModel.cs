namespace Election.Models
{
    namespace Election.Models
    {
        public class CandidateModel
        {
            public int Id { get; set; } // Unique identifier for the candidate
            public string? Name { get; set; } // Candidate's name
            public string? Party { get; set; } // Party affiliation (if applicable)
            public string? Position { get; set; } // Position the candidate is contesting for
            public string? PhotoPath { get; set; } // Path or URL to the candidate's photo
        }
    }

}
