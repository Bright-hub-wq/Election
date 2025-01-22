//using Election.Db;
//using Election.Models;
//using Election.ViewModel;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Election.Controllers
//{
//    public class VoteController : Controller
//    {
//        private readonly AppDbContext _context;

//        public VoteController(AppDbContext context)
//        {
//            _context = context;

//        }

//        public IActionResult VoterDashboard()
//        {
//            return View();
//        }
//        public IActionResult ViewElections()
//        {
//            return View();
//        }
//        public IActionResult ViewHistory()
//        {
//            return View();
//        }
//        public IActionResult ViewResults()
//        {
//            return View();
//        }

//        public IActionResult Index()
//        {
//            var voterRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Voter")?.Id;

//            if (voterRoleId == null)
//            {
//                return View(new List<ApplicationUserViewModel>()); // Empty list
//            }

//            var voters = (from user in _context.ApplicationUser
//                          join userRole in _context.UserRoles on user.Id equals userRole.UserId
//                          where userRole.RoleId == voterRoleId && !user.IsDeactivated
//                          select new ApplicationUserViewModel
//                          {
//                              UserId = user.Id,
//                              FirstName = user.FirstName,
//                              LastName = user.LastName,
//                              DateOfBirth = user.DateOfBirth,
//                              Email = user.Email,
//                              Gender = user.Gender
//                          }).ToList();

//            return View(voters);
//        }






//        [HttpGet]
//        public async Task<IActionResult> Delete(string userId)
//        {
//            if (string.IsNullOrEmpty(userId))
//            {
//                TempData["ErrorMessage"] = "User ID is null or empty.";
//                return RedirectToAction(nameof(Index)); // Redirect to Index instead of showing NotFound
//            }

//            var voter = await _context.ApplicationUser
//                .AsNoTracking()
//                .FirstOrDefaultAsync(v => v.Id == userId);

//            if (voter == null)
//            {
//                TempData["ErrorMessage"] = $"No user found with ID {userId}.";
//                return RedirectToAction(nameof(Index));
//            }

//            // Map to ViewModel
//            var viewModel = new ApplicationUserViewModel
//            {
//                UserId = voter.Id,
//                FirstName = voter.FirstName,
//                LastName = voter.LastName,
//                Email = voter.Email
//            };

//            return View(viewModel);
//        }




//        [HttpPost]
//        public async Task<IActionResult> DeleteConfirmed(string userId)
//        {
//            if (string.IsNullOrEmpty(userId))
//            {
//                TempData["ErrorMessage"] = "User ID is null or empty.";
//                return RedirectToAction(nameof(Index));
//            }

//            try
//            {
//                var user = await _context.ApplicationUser.FindAsync(userId);

//                if (user == null)
//                {
//                    TempData["ErrorMessage"] = $"No user found with ID {userId}.";
//                    return RedirectToAction(nameof(Index));
//                }

//                _context.ApplicationUser.Remove(user);
//                await _context.SaveChangesAsync();

//                TempData["SuccessMessage"] = $"Successfully deleted user: {user.FirstName} {user.LastName}.";
//            }
//            catch (Exception ex)
//            {
//                TempData["ErrorMessage"] = $"An error occurred while deleting the user: {ex.Message}";
//            }

//            return RedirectToAction(nameof(Index));
//        }



//    }




//}



using Election.Db;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Election.Controllers
{
    public class VoteController : Controller
    {
        private readonly AppDbContext _context;

        public VoteController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult VoterDashboard()
        {
            return View();
        }
        //public IActionResult ViewElections()
        //{
        //    return View();
        //}
        public IActionResult ViewHistory()
        {
            return View();
        }
        //public IActionResult ViewResults()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            var voterRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Voter")?.Id;

            if (voterRoleId == null)
            {
                return View(new List<ApplicationUserViewModel>()); // Empty list
            }

            var voters = (from user in _context.ApplicationUser
                          join userRole in _context.UserRoles on user.Id equals userRole.UserId
                          where userRole.RoleId == voterRoleId && !user.IsDeactivated
                          select new ApplicationUserViewModel
                          {
                              UserId = user.Id,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              DateOfBirth = user.DateOfBirth,
                              Email = user.Email,
                              Gender = user.Gender
                          }).ToList();

            return View(voters);
        }

        public async Task<IActionResult> ViewElections()
        {
            var voterId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var elections = await _context.Elections
                .Select(e => new ElectionViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Status = DateTime.Now < e.StartDate ? "Upcoming" :
                             DateTime.Now > e.EndDate ? "Ended" : "Ongoing",
                    HasVoted = _context.Vote.Any(v => v.ElectionId == e.Id && v.VoterId == voterId)
                }).ToListAsync();

            return View(elections);
        }


        public async Task<IActionResult> ReleasedElections()
        {
            // Fetch elections with released results
            var releasedElections = await _context.Elections
                .Where(e => e.ResultsReleased)
                .Select(e => new ElectionViewModel
                {
                    Id = e.Id,
                    Title = e.Title
                }).ToListAsync();

            if (!releasedElections.Any())
            {
                TempData["ErrorMessage"] = "";
                return View("NoResults"); // Show a dedicated view for this case
            }

            return View(releasedElections);
        }






        public async Task<IActionResult> ViewWinner(int electionId)
        {
            // Fetch the election and its winner
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId && e.ResultsReleased);

            if (election == null)
            {
                TempData["ErrorMessage"] = "Election not found or results not released.";
                return RedirectToAction("ViewWinner");
            }

            var winner = election.Candidates
                .OrderByDescending(c => c.VoteCount)
                .FirstOrDefault();

            if (winner == null)
            {
                TempData["ErrorMessage"] = "No winner found for this election.";
                return RedirectToAction("ViewWinner");
            }

            var winnerViewModel = new WinnerViewModel
            {
                Name = winner.Name,
                Party = winner.Party,
                Photopath = winner.PhotoPath,
                VoteCount = winner.VoteCount
            };

            return View(winnerViewModel);
        }




        //public async Task<IActionResult> ViewElections()
        //{
        //    // Fetch elections without candidates
        //    var elections = await _context.Elections
        //        .Select(e => new ElectionViewModel
        //        {
        //            Id = e.Id,
        //            Title = e.Title,
        //            Description = e.Description,
        //            StartDate = e.StartDate,
        //            EndDate = e.EndDate,
        //            Status = DateTime.Now < e.StartDate ? "Upcoming" :
        //                     DateTime.Now > e.EndDate ? "Ended" : "Ongoing"
        //        }).ToListAsync();

        //    return View(elections);
        //}

        //public IActionResult VotingPage(int electionId)
        //{
        //    var election = _context.Elections
        //        .Include(e => e.ElectionCandidates)
        //        .ThenInclude(ec => ec.Candidate)
        //        .FirstOrDefault(e => e.Id == electionId);

        //    if (election == null)
        //    {
        //        TempData["ErrorMessage"] = "Election not found.";
        //        return RedirectToAction("AvailableElections");
        //    }

        //    var model = new VotingViewModel
        //    {
        //        ElectionId = election.Id,
        //        ElectionTitle = election.Title,
        //        ElectionDescription = election.Description,
        //        Candidates = election.ElectionCandidates.Select(ec => new CandidateViewModel
        //        {
        //            Id = ec.CandidateId,
        //            Name = ec.Candidate.Name,
        //            Party = ec.Candidate.Party,
        //            Description = ec.Candidate.Description,
        //            PhotoPath = ec.Candidate.PhotoPath
        //        }).ToList()
        //    };

        //    return View("Vote", model);
        //}


        public IActionResult VotingPage(int electionId)
        {
            // Log all user claims for debugging
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine("User Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine(claim);
            }

            var election = _context.Elections
                .Include(e => e.ElectionCandidates)
                .ThenInclude(ec => ec.Candidate)
                .FirstOrDefault(e => e.Id == electionId);

            if (election == null)
            {
                return RedirectToAction("Error"); // Handle missing election
            }

            var model = new VotingViewModel
            {
                ElectionId = election.Id,
                ElectionTitle = election.Title,
                ElectionDescription = election.Description,
                Candidates = election.ElectionCandidates.Select(ec => new CandidateViewModel
                {
                    Id = ec.CandidateId,
                    Name = ec.Candidate.Name,
                    Party = ec.Candidate.Party,
                    Description = ec.Candidate.Description,
                    PhotoPath = ec.Candidate.PhotoPath
                }).ToList()
            };

            return View("Vote", model);
        }


        //[HttpPost]
        //public async Task<IActionResult> Vote(int candidateId, int electionId)
        //{
        //    var voterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    // Check if voter has already voted in this election
        //    var hasVoted = _context.Vote.Any(v => v.ElectionId == electionId && v.VoterId == voterId);

        //    if (hasVoted)
        //    {
        //        TempData["ErrorMessage"] = "You have already voted in this election.";
        //        return RedirectToAction("ViewElections", new { electionId });
        //    }

        //    // Save the vote
        //    var vote = new Vote
        //    {
        //        CandidateId = candidateId,
        //        ElectionId = electionId,
        //        VoterId = voterId,
        //        VotedAt = DateTime.Now
        //    };

        //    _context.Vote.Add(vote);
        //    await _context.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "Thank you for voting!";
        //    return RedirectToAction("ViewElections", new { electionId });
        //}



        [HttpPost]
        public async Task<IActionResult> Vote(int candidateId, int electionId)
        {
            // Get the VoterId (this assumes you have a method or property to get the VoterId)
            var voterId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // or get it from your custom logic

            if (string.IsNullOrEmpty(voterId))
            {
                // If the voter is not authenticated, redirect to login
                TempData["ErrorMessage"] = "You need to log in to vote.";
                return RedirectToAction("Login", "Account"); // Adjust to your login route
            }

            // Check if the voter has already voted in this election
            var existingVote = await _context.Vote
                                              .FirstOrDefaultAsync(v => v.VoterId == voterId && v.ElectionId == electionId);
            if (existingVote != null)
            {
                // If already voted, show error message
                TempData["ErrorMessage"] = "You have already voted in this election.";
                return RedirectToAction("Vote", new { electionId });
            }

            // Check if the candidate exists in the database
            var candidate = await _context.Candidates.FindAsync(candidateId);
            if (candidate == null)
            {
                TempData["ErrorMessage"] = "Invalid candidate.";
                return RedirectToAction("Vote", new { electionId });
            }

            // Create a new vote
            var vote = new Vote
            {
                CandidateId = candidateId,
                ElectionId = electionId,
                VoterId = voterId,  // Use the VoterId here instead of UserId
                VoteDate = DateTime.Now // Record the vote timestamp
            };

            try
            {
                // Add the new vote to the Votes table
                _context.Vote.Add(vote);
                await _context.SaveChangesAsync(); // Save changes to the database

                // Increment the vote count for the candidate
                candidate.VoteCount += 1; // Assuming `VoteCount` is the column for the votes

                // Save the updated candidate to the database
                _context.Candidates.Update(candidate);
                await _context.SaveChangesAsync(); // Save changes to update the vote count

                // Show success message
                TempData["SuccessMessage"] = "Your vote has been successfully recorded.";
            }
            catch (Exception ex)
            {
                // Handle any database errors
                TempData["ErrorMessage"] = "An error occurred while recording your vote. Please try again.";
                // Log the exception as needed
            }

            // Redirect to the elections page after voting
            return RedirectToAction("ViewElections"); // Adjust this to your elections page route
        }













        //[HttpPost]
        //public async Task<IActionResult> Vote(int candidateId, int electionId)
        //{
        //    // Fetch the voter ID from the current user claims
        //    var voterId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        //    // Ensure voterId is fetched
        //    if (string.IsNullOrEmpty(voterId))
        //    {
        //        TempData["ErrorMessage"] = "Unable to fetch your voter ID. Please log in again.";
        //        return RedirectToAction("Login", "Account");
        //    }

        //    Console.WriteLine($"Fetched Voter ID: {voterId}"); // Log the voter ID for debugging

        //    // Validate the election exists
        //    var election = await _context.Elections
        //        .Include(e => e.Candidates)
        //        .FirstOrDefaultAsync(e => e.Id == electionId);

        //    if (election == null)
        //    {
        //        TempData["ErrorMessage"] = "Invalid election.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    // Validate the candidate belongs to the election
        //    var candidate = election.Candidates.FirstOrDefault(c => c.Id == candidateId);

        //    if (candidate == null)
        //    {
        //        TempData["ErrorMessage"] = "Invalid candidate selection.";
        //        return RedirectToAction("VotingPage", new { electionId });
        //    }

        //    // Check if the voter has already voted in this election
        //    var hasVoted = await _context.Vote
        //        .AnyAsync(v => v.ElectionId == electionId && v.VoterId == voterId);

        //    if (hasVoted)
        //    {
        //        TempData["ErrorMessage"] = "You have already voted in this election.";
        //        return RedirectToAction("VotingPage", new { electionId });
        //    }

        //    // Save the vote to the database
        //    var vote = new Vote
        //    {
        //        CandidateId = candidateId,
        //        ElectionId = electionId,
        //        VoterId = voterId,
        //        VotedAt = DateTime.Now
        //    };

        //    _context.Vote.Add(vote);
        //    await _context.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "Thank you for voting!";
        //    return RedirectToAction("ViewElections");
        //}




        //[HttpPost]
        //public async Task<IActionResult> CastVote(int electionId)
        //{
        //    // Ensure the user is authenticated
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        TempData["ErrorMessage"] = "You must be logged in to vote.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    // Get the voter ID
        //    var voterId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(voterId))
        //    {
        //        TempData["ErrorMessage"] = "Could not retrieve your voter ID. Please log in again.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    // Check if the voter already voted
        //    var existingVote = await _context.Vote
        //        .FirstOrDefaultAsync(v => v.ElectionId == electionId && v.VoterId == voterId);

        //    if (existingVote != null)
        //    {
        //        TempData["ErrorMessage"] = "You have already voted in this election.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    // Save the vote
        //    var vote = new Vote
        //    {
        //        ElectionId = electionId,
        //        VoterId = voterId,
        //        VoteTime = DateTime.Now
        //    };

        //    _context.Vote.Add(vote);
        //    await _context.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "Your vote has been cast successfully!";
        //    return RedirectToAction("ViewElections");
        //}







        public async Task<IActionResult> VoteNow(int electionId)
        {
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            if (election == null)
            {
                TempData["ErrorMessage"] = "Election not found.";
                return RedirectToAction("ViewElections");
            }

            if (election.Candidates == null || !election.Candidates.Any())
            {
                TempData["ErrorMessage"] = "No candidates found for this election.";
                return RedirectToAction("ViewElections");
            }

            var viewModel = new VoteNowViewModel
            {
                ElectionId = election.Id,
                ElectionTitle = election.Title,
                Candidates = election.Candidates.Select(c => new CandidateViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Party = c.Party,
                    PhotoPath = c.PhotoPath
                }).ToList()

            };

            return View(viewModel);
        }




        [HttpPost]
        public IActionResult SubmitVote(int electionId, int candidateId)
        {
            var voterId = User.Identity.Name; // Ensure the user is logged in

            // Check if the user has already voted in this election
            var hasVoted = _context.Vote.Any(v => v.ElectionId == electionId && v.VoterId == voterId);
            if (hasVoted)
            {
                TempData["Error"] = "You have already voted in this election.";
                return RedirectToAction("Vote", new { electionId });
            }

            // Save the vote
            var vote = new Vote
            {
                ElectionId = electionId,
                CandidateId = candidateId,
                VoterId = voterId
            };

            _context.Vote.Add(vote);
            _context.SaveChanges();

            TempData["Success"] = "Your vote has been successfully cast!";
            return RedirectToAction("Vote", new { electionId });
        }













        [HttpPost]
        public async Task<IActionResult> ConfirmVote(int candidateId)
        {
            var candidate = await _context.Candidates.FindAsync(candidateId);

            if (candidate == null)
            {
                TempData["ErrorMessage"] = "Candidate not found.";
                return RedirectToAction("ViewElections");
            }

            // Increment the vote count for the candidate
            candidate.Votes++;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your vote has been submitted successfully!";
            return RedirectToAction("ViewElections");
        }



        //public async Task<IActionResult> ViewElections()
        //{
        //    // Fetch elections with their candidates
        //    var elections = await _context.Elections
        //        .Include(e => e.Candidates) // Include candidates in the query
        //        .Select(e => new ElectionViewModel
        //        {
        //            Id = e.Id,
        //            Title = e.Title,
        //            Description = e.Description,
        //            StartDate = e.StartDate,
        //            EndDate = e.EndDate,
        //            Status = DateTime.Now < e.StartDate ? "Upcoming" :
        //                     DateTime.Now > e.EndDate ? "Ended" : "Ongoing",
        //            Candidates = e.Candidates.Select(c => new CandidateViewModel
        //            {
        //                Id = c.Id,
        //                Name = c.Name,
        //                Party = c.Party,
        //                PhotoPath = c.PhotoPath
        //            }).ToList()
        //        }).ToListAsync();

        //    // Return the view with the fetched data
        //    return View(elections);
        //}




        public async Task<IActionResult> ViewResults(int electionId)
        {
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            if (election == null)
            {
                return NotFound("Election not found.");
            }

            // Check if the election has ended
            if (DateTime.Now < election.EndDate)
            {
                TempData["ErrorMessage"] = "The election is still ongoing.";
                return RedirectToAction("ViewElections");
            }

            var results = election.Candidates
                .OrderByDescending(c => c.Votes) // Order candidates by vote count
                .ToList();

            return View(results);
        }


        //public async Task<IActionResult> VoteNow(int candidateId)
        //{
        //    var candidate = await _context.Candidates
        //        .Include(c => c.Election) // Optional: Include election for additional context
        //        .FirstOrDefaultAsync(c => c.Id == candidateId);

        //    if (candidate == null)
        //    {
        //        return NotFound("Candidate not found.");
        //    }

        //    // Pass the candidate to the voting page
        //    return View("VoteNow", candidate);
        //}


        //[HttpPost]
        //public async Task<IActionResult> ConfirmVote(int candidateId)
        //{
        //    var candidate = await _context.Candidates
        //        .Include(c => c.Election)
        //        .FirstOrDefaultAsync(c => c.Id == candidateId);

        //    if (candidate == null)
        //    {
        //        return NotFound("Candidate not found.");
        //    }

        //    // Check if the voter has already voted in this election
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user’s ID
        //    var hasVoted = await _context.Vote
        //        .AnyAsync(v => v.UserId == userId && v.ElectionId == candidate.ElectionId);

        //    if (hasVoted)
        //    {
        //        TempData["ErrorMessage"] = "You have already voted in this election.";
        //        return RedirectToAction("ViewElections");
        //    }
        //    else
        //    {
        //        TempData["SuccessMessage"] = "Your vote has been successfully submitted!";

        //    }
        //    // Logic to save the vote
        //    candidate.Votes++;
        //    _context.Vote.Add(new Vote
        //    {
        //        UserId = userId,
        //        CandidateId = candidateId,
        //        ElectionId = (int)candidate.ElectionId
        //    });

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("ViewElections");
        //}







        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User ID is null or empty.";
                return RedirectToAction(nameof(Index)); // Redirect to Index instead of showing NotFound
            }

            var voter = await _context.ApplicationUser
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == userId);

            if (voter == null)
            {
                TempData["ErrorMessage"] = $"No user found with ID {userId}.";
                return RedirectToAction(nameof(Index));
            }

            // Map to ViewModel
            var viewModel = new ApplicationUserViewModel
            {
                UserId = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                Email = voter.Email
            };

            return View(viewModel);
        }





        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User ID is null or empty.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var user = await _context.ApplicationUser.FindAsync(userId);

                if (user == null)
                {
                    TempData["ErrorMessage"] = $"No user found with ID {userId}.";
                    return RedirectToAction(nameof(Index));
                }

                _context.ApplicationUser.Remove(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully deleted user: {user.FirstName} {user.LastName}.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the user: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }



    }


}


