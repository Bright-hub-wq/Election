//using Election.Db;
//using Election.ViewModel;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Election.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly AppDbContext _context;

//        public AdminController(AppDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult AdminDashBoard() => View();
//        public IActionResult ManageUsers() => View();
//        public IActionResult ManageElections() => View();
//        public IActionResult CountVotes() => View();
//        public IActionResult ManageResults() => View();
//        public IActionResult Details() => View();

//        // Display all candidates
//        public IActionResult ManageCandidates()
//        {
//            var candidates = _context.Candidates.ToList();
//            return View(candidates);
//        }
//        public IActionResult ElectionSettings()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult SaveElectionSettings(string ElectionName, DateTime StartDate, DateTime EndDate)
//        {
//            // Save settings to the database or a configuration file
//            // Example: Save to TempData for simplicity
//            TempData["ElectionName"] = ElectionName;
//            TempData["StartDate"] = StartDate.ToString("yyyy-MM-dd");
//            TempData["EndDate"] = EndDate.ToString("yyyy-MM-dd");

//            ViewBag.Message = "Election settings saved successfully.";
//            return RedirectToAction("ElectionSettings");
//        }

//        // Add a candidate
//        [HttpGet]
//        public IActionResult AddCandidate()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult AddCandidate(Candidate candidate, IFormFile photo)
//        {
//            if (ModelState.IsValid)
//            {
//                if (photo != null)
//                {
//                    var photoPath = Path.Combine("wwwroot/uploads", photo.FileName);
//                    using (var stream = new FileStream(photoPath, FileMode.Create))
//                    {
//                        photo.CopyTo(stream);
//                    }
//                    candidate.PhotoPath = "/uploads/" + photo.FileName;
//                }

//                _context.Candidates.Add(candidate);
//                _context.SaveChanges();
//                return RedirectToAction(nameof(ManageCandidates));
//            }
//            return View(candidate);
//        }

//        public IActionResult PushToElection(int id)
//        {
//            var candidate = _context.Candidates.Find(id);
//            if (candidate == null)
//            {
//                TempData["ErrorMessage"] = "Candidate not found.";
//                return RedirectToAction("ManageCandidates");
//            }

//            candidate.IsInElection = true; // Add this field to the Candidate model
//            _context.SaveChanges();
//            TempData["SuccessMessage"] = "Candidate added to the election successfully.";
//            return RedirectToAction("ManageCandidates");
//        }

//        public IActionResult ViewResults()
//        {
//            var results = _context.Candidates
//                .Where(c => c.IsInElection)
//                .OrderByDescending(c => c.Votes)
//                .Select(c => new
//                {
//                    c.Name,
//                    c.Party,
//                    c.Position,
//                    c.Votes
//                })
//                .ToList();

//            return View(results);
//        }

//        [HttpPost]
//        public IActionResult CastVote(int candidateId, string UserId)
//        {
//            // Check if voter exists and hasn't voted yet
//            var voter = _context.Vote.FirstOrDefault(v => v.UserId == UserId);
//            if (voter == null || voter.HasVoted)
//            {
//                TempData["ErrorMessage"] = "You have already voted or voter ID is invalid.";
//                return RedirectToAction("VotingPage");
//            }

//            // Update the candidate's votes
//            var candidate = _context.Candidates.Find(candidateId);
//            if (candidate == null || !candidate.IsInElection)
//            {
//                TempData["ErrorMessage"] = "Invalid candidate.";
//                return RedirectToAction("VotingPage");
//            }

//            candidate.Votes++;
//            voter.HasVoted = true;

//            _context.SaveChanges();
//            TempData["SuccessMessage"] = "Vote cast successfully.";
//            return RedirectToAction("VotingPage");
//        }

//        ////public IActionResult EditCandidate(int id)
//        ////{
//        ////    var candidate = _context.Candidates.Find(id);
//        ////    return View(candidate);
//        ////}

//        //[HttpPost]
//        //public IActionResult EditCandidate(Candidate candidate)
//        //{
//        //    _context.Candidates.Update(candidate);
//        //    _context.SaveChanges();
//        //    return RedirectToAction("ManageCandidates");
//        //}

//        //public IActionResult DeleteCandidate(int id)
//        //{
//        //    var candidate = _context.Candidates.Find(id);
//        //    if (candidate != null)
//        //    {
//        //        _context.Candidates.Remove(candidate);
//        //        _context.SaveChanges();
//        //    }
//        //    return RedirectToAction("ViewCandidates");
//        //}

//        ////public IActionResult DeleteCandidate(int id)
//        ////{
//        ////    var candidate = _context.Candidates.Find(id);
//        ////    if (candidate == null)
//        ////    {
//        ////        return NotFound();
//        ////    }
//        ////    return View(candidate);
//        ////}

//        ////[HttpPost]
//        ////public IActionResult DeleteCandidateConfirmed(int id)
//        ////{
//        ////    var candidate = _context.Candidates.Find(id);
//        ////    if (candidate != null)
//        ////    {
//        ////        _context.Candidates.Remove(candidate);
//        ////        _context.SaveChanges();
//        ////    }
//        ////    return RedirectToAction("ViewCandidates");
//        ////}

//        public IActionResult DeleteCandidate(int id)
//        {
//            var candidate = _context.Candidates.Find(id);
//            if (candidate == null)
//            {
//                TempData["ErrorMessage"] = "The candidate does not exist.";
//                return RedirectToAction("ManageCandidates");
//            }
//            return View(candidate);
//        }

//        ////[HttpPost]
//        ////public IActionResult DeleteCandidateConfirmed(int id)
//        ////{
//        ////    var candidate = _context.Candidates.Find(id);
//        ////    if (candidate != null)
//        ////    {
//        ////        // Optional: Check if candidate is associated with an ongoing election
//        ////        var hasOngoingElection = _context.Vote.Any(e => e.CandidateId == id && e.IsOngoing);
//        ////        if (hasOngoingElection)
//        ////        {
//        ////            TempData["ErrorMessage"] = "This candidate is associated with an ongoing election and cannot be deleted.";
//        ////            return RedirectToAction("ViewCandidates");
//        ////        }

//        ////        _context.Candidates.Remove(candidate);
//        ////        _context.SaveChanges();
//        ////        TempData["SuccessMessage"] = "Candidate deleted successfully.";
//        ////    }
//        ////    else
//        ////    {
//        ////        TempData["ErrorMessage"] = "The candidate could not be found.";
//        ////    }
//        ////    return RedirectToAction("ViewCandidates");
//        ////}
//        [HttpPost]
//        public IActionResult DeleteCandidateConfirmed(int id)
//        {
//            var candidate = _context.Candidates.Find(id);
//            if (candidate != null)
//            {
//                _context.Candidates.Remove(candidate);
//                _context.SaveChanges();
//                TempData["SuccessMessage"] = "Candidate deleted successfully.";
//            }
//            else
//            {
//                TempData["ErrorMessage"] = "The candidate could not be found.";
//            }
//            return RedirectToAction("ManageCandidates");
//        }

//        // GET: Edit Candidate Form
//        [HttpGet]
//        public IActionResult EditCandidate(int id)
//        {
//            var candidate = _context.Candidates.Find(id);
//            if (candidate == null)
//            {
//                return NotFound();
//            }
//            return View(candidate);
//        }

//        // POST: Submit Edits to Candidate
//        [HttpPost]
//        public IActionResult EditCandidate(Candidate candidate, IFormFile photo)
//        {
//            if (ModelState.IsValid)
//            {
//                var existingCandidate = _context.Candidates.Find(candidate.Id);
//                if (existingCandidate != null)
//                {
//                    // Update candidate details
//                    existingCandidate.Name = candidate.Name;
//                    existingCandidate.Party = candidate.Party;
//                    existingCandidate.Position = candidate.Position;
//                    existingCandidate.Description = candidate.Description;

//                    // Handle photo upload
//                    if (photo != null)
//                    {
//                        var photoPath = Path.Combine("wwwroot/uploads", photo.FileName);
//                        using (var stream = new FileStream(photoPath, FileMode.Create))
//                        {
//                            photo.CopyTo(stream);
//                        }
//                        existingCandidate.PhotoPath = "/uploads/" + photo.FileName;
//                    }

//                    _context.SaveChanges();
//                    return RedirectToAction(nameof(ManageCandidates));
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Candidate not found.");
//                }
//            }
//            return View(candidate);
//        }


//        //// Edit a candidate
//        //[HttpGet]
//        //public IActionResult EditCandidate(int id)
//        //{
//        //    var candidate = _context.Candidates.Find(id);
//        //    if (candidate == null) return NotFound();
//        //    return View(candidate);
//        //}

//        ////[HttpPost]
//        ////public IActionResult EditCandidate(Candidate candidate, IFormFile photo)
//        ////{
//        ////    if (ModelState.IsValid)
//        ////    {
//        ////        var existingCandidate = _context.Candidates.Find(candidate.Id);
//        ////        if (existingCandidate != null)
//        ////        {
//        ////            existingCandidate.Name = candidate.Name;
//        ////            existingCandidate.Party = candidate.Party;
//        ////            existingCandidate.Position = candidate.Position;
//        ////            existingCandidate.Description = candidate.Description;

//        ////            if (photo != null)
//        ////            {
//        ////                var photoPath = Path.Combine("wwwroot/uploads", photo.FileName);
//        ////                using (var stream = new FileStream(photoPath, FileMode.Create))
//        ////                {
//        ////                    photo.CopyTo(stream);
//        ////                }
//        ////                existingCandidate.PhotoPath = "/uploads/" + photo.FileName;
//        ////            }

//        ////            _context.SaveChanges();
//        ////            return RedirectToAction(nameof(ManageCandidates));
//        ////        }
//        ////    }
//        ////    return View(candidate);

//        ////}


//    }

//    //public class AdminController : Controller
//    //{
//    //    public IActionResult AdminDashBoard() => View();
//    //    public IActionResult ManageUsers() => View();
//    //    public IActionResult ManageElections() => View();
//    //    public IActionResult CountVotes() => View();
//    //    public IActionResult ManageResults() => View();
//    //    public IActionResult Details() => View();








//    //}


//}



using Election.Db;
using Election.Models;
using Election.Models.Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Election.Controllers
{
    public class AdminController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment; // Inject IWebHostEnvironment here
        }


        public IActionResult AdminDashBoard() => View();
        public IActionResult ManageUsers() => View();
        public IActionResult ManageElections() => View();
        public IActionResult CountVotes() => View();
        //public IActionResult ManageResults() => View();
        public IActionResult Details() => View();

        // Display all candidates
        public IActionResult ManageCandidates()
        {
            var candidates = _context.Candidates.ToList();
            return View(candidates);
        }
        public IActionResult ElectionSettings()
        {
            return View();
        }


        public async Task<IActionResult> ManageResults()
        {
            // Fetch all elections to display
            var elections = await _context.Elections
                .Select(e => new ElectionViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    HasEnded = e.EndDate < DateTime.Now,
                    ResultsReleased = e.ResultsReleased
                }).ToListAsync();

            return View(elections);
        }



        public async Task<IActionResult> ReleaseResults(int electionId)
        {
            // Fetch the election along with its candidates
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            // Check if the election exists
            if (election == null)
            {
                TempData["ErrorMessage"] = "Election not found.";
                return RedirectToAction("ManageResults");
            }

            // Check if the election has ended
            if (election.EndDate > DateTime.UtcNow) // Replace EndDate with the actual property
            {
                TempData["ErrorMessage"] = "Election has not yet ended.";
                return RedirectToAction("ManageResults");
            }

            // Fetch vote counts and prepare results
            var results = election.Candidates
                .Select(c => new CandidateResultViewModel
                {
                    CandidateId = c.Id,
                    Name = c.Name,
                    Party = c.Party,
                    Photopath = c.PhotoPath, // Ensure the property names match
                    VoteCount = c.VoteCount
                })
                .OrderByDescending(r => r.VoteCount) // Sort by vote count in descending order
                .ToList();

            // Mark the election results as released
            election.ResultsReleased = true;
            _context.Elections.Update(election);
            await _context.SaveChangesAsync();

            // Pass results to the ElectionResults view
            return View("ElectionResults", results);
        }





        public async Task<IActionResult> ViewElections()
        {
            var elections = await _context.Elections
                .Include(e => e.ElectionCandidates)
                    .ThenInclude(ec => ec.Candidate) // Eagerly load the candidates
                .ToListAsync();

            var viewModel = elections.Select(e => new ElectionViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Candidates = e.ElectionCandidates.Select(ec => new CandidateViewModel
                {
                    Id = ec.Candidate.Id,
                    Name = ec.Candidate.Name,
                    Party = ec.Candidate.Party,
                    PhotoPath = ec.Candidate.PhotoPath
                }).ToList(),
                Status = e.IsOngoing() ? "Ongoing" : e.HasEnded() ? "Ended" : "Upcoming"
            }).ToList();

            return View(viewModel);
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
                return RedirectToAction("ReleasedElections");
            }

            var winner = election.Candidates
                .OrderByDescending(c => c.VoteCount)
                .FirstOrDefault();

            if (winner == null)
            {
                TempData["ErrorMessage"] = "No winner found for this election.";
                return RedirectToAction("ReleasedElections");
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


        //public async Task<IActionResult> ViewWinner(int id)
        //{
        //    var election = await _context.Elections
        //        .Include(e => e.Candidates)
        //        .FirstOrDefaultAsync(e => e.Id == id);

        //    if (election == null || election.Status != "Results Released")
        //    {
        //        return NotFound();
        //    }

        //    var winner = election.Candidates.OrderByDescending(c => c.VoteCount).FirstOrDefault();

        //    var viewModel = new WinnerViewModel
        //    {
        //        ElectionTitle = election.Title,
        //        Description = election.Description,
        //        StartDate = election.StartDate,
        //        EndDate = election.EndDate,
        //        WinnerName = winner?.Name ?? "No Winner",
        //        WinnerPhotoPath = winner?.PhotoPath ?? string.Empty,
        //        WinnerParty = winner?.Party ?? "N/A",
        //        WinnerVoteCount = winner?.VoteCount ?? 0
        //    };

        //    return View(viewModel);
        //}







        //[HttpPost]
        //public async Task<IActionResult> DeleteElection(int id)
        //{
        //    var election = await _context.Elections.FindAsync(id);
        //    if (election == null || !election.HasEnded())
        //    {
        //        return NotFound("Election not found or cannot be deleted.");
        //    }

        //    _context.Elections.Remove(election);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(ViewElections));
        //}

        [HttpGet]
        public async Task<IActionResult> EditElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null || election.StartDate <= DateTime.Now)
            {
                return NotFound("Election not found or cannot be edited.");
            }

            return View(election);
        }


        public async Task<IActionResult> EndElection(int electionId)
        {
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            if (election == null)
            {
                return NotFound();
            }

            // Check if the election has already ended
            if (election.Status == "Ended")
            {
                return RedirectToAction(nameof(ViewElections)); // Replace with correct action name
            }

            // Set election status to "Ended"
            election.Status = "Ended";  // Ensure the 'Status' field is properly defined in your model
            _context.Update(election);
            await _context.SaveChangesAsync();

            // Count the votes for each candidate and update the vote count
            foreach (var candidate in election.Candidates)
            {
                var voteCount = await _context.Vote
                    .Where(v => v.CandidateId == candidate.Id && v.ElectionId == electionId)
                    .CountAsync();

                // Update the candidate's vote count
                candidate.Votes = voteCount;
                _context.Update(candidate);
            }

            // Save changes to update the vote counts
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewElections)); // Adjust as needed
        }




        [HttpPost]
        public IActionResult SaveElection(ElectionModel election)
        {
            if (ModelState.IsValid)
            {
                _context.Elections.Add(election);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Election created successfully!";
                return RedirectToAction("ManageElections");
            }

            TempData["ErrorMessage"] = "Failed to create election. Please check the details.";
            return View("ElectionSettings");
        }


        [HttpGet]
        public IActionResult CreateCandidate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCandidate(CreateCandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload logic
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.Photo?.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                var candidate = new Candidate
                {
                    Name = model.Name,
                    Party = model.Party,
                    Position = model.Position,
                    Description = model.Description,
                    PhotoPath = "/images/" + model.Photo.FileName // Save file path
                };

                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                // Use TempData to display a success message on the next page
                TempData["SuccessMessage"] = "Candidate created successfully.";

                // Redirect to the page where the admin can view all candidates
                return RedirectToAction("ManageCandidates"); // Assumes that the action for viewing candidates is named "Index"
            }

            // If the model is invalid, return the same view with validation errors
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateElection()
        {
            // Fetch all candidates to display in the form
            var candidates = _context.Candidates
                .Select(c => new CandidateViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Party = c.Party,
                    PhotoPath = c.PhotoPath
                }).ToList();

            var viewModel = new CreateElectionViewModel
            {
                AvailableCandidates = candidates
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateElection(CreateElectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // If StartDate is the same as EndDate, set EndDate to the same day as StartDate
                    if (model.StartDate == model.EndDate)
                    {
                        model.EndDate = model.StartDate.AddDays(1).AddSeconds(-1); // Set the end date to the end of the same day
                    }

                    // Create the Election object
                    var election = new ElectionModel
                    {
                        Title = model.Title,
                        Description = model.Description,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate
                    };

                    // Save the election to generate its ID
                    _context.Elections.Add(election);
                    await _context.SaveChangesAsync();

                    // Add selected candidates to the ElectionCandidates table and update ElectionId in Candidates table
                    if (model.SelectedCandidateIds != null && model.SelectedCandidateIds.Any())
                    {
                        foreach (var candidateId in model.SelectedCandidateIds)
                        {
                            // Add to ElectionCandidates table
                            var electionCandidate = new ElectionCandidate
                            {
                                ElectionId = election.Id,
                                CandidateId = candidateId
                            };
                            _context.ElectionCandidates.Add(electionCandidate);

                            // Update the ElectionId in the Candidates table
                            var candidate = await _context.Candidates.FindAsync(candidateId);
                            if (candidate != null)
                            {
                                candidate.ElectionId = election.Id; // Assuming ElectionId exists in the Candidates table
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    TempData["SuccessMessage"] = "Election created successfully!";
                    return RedirectToAction("ViewElections");
                }
                catch (Exception ex)
                {
                    // Log the error (you can use a logging framework here)
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Error");
                }
            }

            // If invalid, reload available candidates
            model.AvailableCandidates = await _context.Candidates
                .Select(c => new CandidateViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Party = c.Party,
                    PhotoPath = c.PhotoPath
                })
                .ToListAsync();

            return View(model);
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateElection(CreateElectionViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Create the Election object
        //            var election = new ElectionModel
        //            {
        //                Title = model.Title,
        //                Description = model.Description,
        //                StartDate = model.StartDate,
        //                EndDate = model.EndDate
        //            };

        //            // Save the election to generate its ID
        //            _context.Elections.Add(election);
        //            await _context.SaveChangesAsync();

        //            // Add selected candidates to the ElectionCandidates table
        //            if (model.SelectedCandidateIds != null && model.SelectedCandidateIds.Any())
        //            {
        //                foreach (var candidateId in model.SelectedCandidateIds)
        //                {
        //                    var electionCandidate = new ElectionCandidate
        //                    {
        //                        ElectionId = election.Id,
        //                        CandidateId = candidateId
        //                    };
        //                    _context.ElectionCandidates.Add(electionCandidate);
        //                }

        //                await _context.SaveChangesAsync();
        //            }

        //            TempData["SuccessMessage"] = "Election created successfully!";
        //            return RedirectToAction("ViewElections");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the error (you can use a logging framework here)
        //            Console.WriteLine(ex.Message);
        //            return RedirectToAction("Error");
        //        }
        //    }

        //    // If invalid, reload available candidates
        //    model.AvailableCandidates = await _context.Candidates
        //        .Select(c => new CandidateViewModel
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Party = c.Party,
        //            PhotoPath = c.PhotoPath
        //        })
        //        .ToListAsync();

        //    return View(model);
        //}




        //public async Task<IActionResult> EditElection(int id)
        //{
        //    var election = await _context.Elections
        //        .Include(e => e.ElectionCandidates)
        //        .ThenInclude(ec => ec.Candidate)
        //        .FirstOrDefaultAsync(e => e.Id == id);

        //    if (election == null)
        //    {
        //        return NotFound();
        //    }

        //    if (DateTime.Now >= election.StartDate)
        //    {
        //        TempData["ErrorMessage"] = "You cannot edit an ongoing or ended election.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    return View(election);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditElection(ElectionModel model)
        {
            var election = await _context.Elections.FindAsync(model.Id);
            if (election == null || DateTime.Now >= election.StartDate)
            {
                TempData["ErrorMessage"] = "Editing is not allowed for ongoing or ended elections.";
                return RedirectToAction("ViewElections");
            }

            election.Title = model.Title;
            election.Description = model.Description;
            election.StartDate = model.StartDate;
            election.EndDate = model.EndDate;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Election updated successfully!";
            return RedirectToAction("ViewElections");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteElection(int id)
        //{
        //    var election = await _context.Elections.FindAsync(id);

        //    if (election == null)
        //    {
        //        TempData["ErrorMessage"] = "Election not found.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    if (election.EndDate >= DateTime.Now)
        //    {
        //        TempData["ErrorMessage"] = "You can only delete elections that have ended.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    _context.Elections.Remove(election);
        //    await _context.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "Election deleted successfully.";
        //    return RedirectToAction("ViewElections");
        //}




        [HttpPost]
        public async Task<IActionResult> DeleteElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null || !election.HasEnded())
            {
                return NotFound("Election not found or cannot be deleted.");
            }

            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewElections));
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidate(Candidate model, IFormFile photo)
        {
            var candidate = await _context.Candidates.FindAsync(model.Id);
            if (candidate == null)
            {
                TempData["ErrorMessage"] = "Candidate not found.";
                return RedirectToAction("ManageCandidates");
            }

            candidate.Name = model.Name;
            candidate.Party = model.Party;
            candidate.Position = model.Position;
            candidate.Description = model.Description;

            // Handle photo upload
            if (photo != null && photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                // Delete old photo if it exists
                if (!string.IsNullOrEmpty(candidate.PhotoPath))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, candidate.PhotoPath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                candidate.PhotoPath = "/images/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Candidate updated successfully.";
            return RedirectToAction("ManageCandidates");
        }



        [HttpGet]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                TempData["ErrorMessage"] = "Candidate not found.";
                return RedirectToAction("ManageCandidates");
            }

            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCandidateConfirmed(int id)
        {
            // Find the candidate in the database
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                TempData["ErrorMessage"] = "Candidate not found.";
                return RedirectToAction("ManageCandidates");
            }

            // Delete the candidate's photo from the server
            if (!string.IsNullOrEmpty(candidate.PhotoPath))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, candidate.PhotoPath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Remove the candidate from the database
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Candidate deleted successfully.";
            return RedirectToAction("ManageCandidates");
        }




        [HttpPost]
        public IActionResult SaveElectionSettings(string Title, DateTime StartDate, DateTime EndDate)
        {
            // Save settings to the database or a configuration file
            // Example: Save to TempData for simplicity
            TempData["ElectionTitle"] = Title;
            TempData["StartDate"] = StartDate.ToString("yyyy-MM-dd");
            TempData["EndDate"] = EndDate.ToString("yyyy-MM-dd");

            ViewBag.Message = "Election settings saved successfully.";
            return RedirectToAction("ElectionSettings");
        }







        public async Task<IActionResult> Vote(int electionId, int candidateId)
        {
            var election = await _context.Elections
                .Include(e => e.Candidates)
                .FirstOrDefaultAsync(e => e.Id == electionId);

            if (election == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null || election.Status != "Ongoing")
            {
                return NotFound();  // Make sure the election is ongoing before allowing voting
            }

            // Check if the user has already voted in this election
            var existingVote = await _context.Vote
                .FirstOrDefaultAsync(v => v.UserId == User.Identity.Name && v.ElectionId == electionId);

            if (existingVote != null)
            {
                // The user has already voted, handle it accordingly (e.g., show a message)
                return RedirectToAction("AlreadyVoted", new { electionId = electionId });
            }

            // Create a new vote
            var vote = new Vote
            {
                ElectionId = electionId,
                CandidateId = candidateId,
                UserId = User.Identity.Name, // Assuming you have a UserId in your context
                VoteDate = DateTime.Now
            };

            // Save the vote
            _context.Vote.Add(vote);
            await _context.SaveChangesAsync();

            // Increment the candidate's vote count
            candidate.Votes++;
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();

            return RedirectToAction("ElectionDetails", new { electionId = electionId });
        }


        public async Task<IActionResult> Index()
        {
            var elections = await _context.Elections
                                          .Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
                                          .Include(e => e.ElectionCandidates)
                                          .ThenInclude(ec => ec.Candidate)
                                          .ToListAsync();

            var viewModel = elections.Select(e => new ElectionVoteViewModel
            {
                ElectionId = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Candidates = e.ElectionCandidates.Select(ec => ec.Candidate)
            }).ToList();

            return View(viewModel);
        }




    }
}










//public IActionResult DeleteCandidate(int id)
//{
//    var candidate = _context.Candidates.Find(id);
//    if (candidate == null)
//    {
//        return NotFound();
//    }
//    return View(candidate);
//}

//[HttpPost]
//public IActionResult DeleteCandidateConfirmed(int id)
//{
//    var candidate = _context.Candidates.Find(id);
//    if (candidate != null)
//    {
//        _context.Candidates.Remove(candidate);
//        _context.SaveChanges();
//    }
//    return RedirectToAction("ViewCandidates");
//}


//public IActionResult DeleteCandidate(int id)
//{
//    var candidate = _context.Candidates.Find(id);
//    if (candidate == null)
//    {
//        TempData["ErrorMessage"] = "The candidate does not exist.";
//        return RedirectToAction("ManageCandidates");
//    }
//    return View(candidate);
//}

////[HttpPost]
////public IActionResult DeleteCandidateConfirmed(int id)
////{
////    var candidate = _context.Candidates.Find(id);
////    if (candidate != null)
////    {
////        // Optional: Check if candidate is associated with an ongoing election
////        var hasOngoingElection = _context.Vote.Any(e => e.CandidateId == id && e.IsOngoing);
////        if (hasOngoingElection)
////        {
////            TempData["ErrorMessage"] = "This candidate is associated with an ongoing election and cannot be deleted.";
////            return RedirectToAction("ViewCandidates");
////        }

////        _context.Candidates.Remove(candidate);
////        _context.SaveChanges();
////        TempData["SuccessMessage"] = "Candidate deleted successfully.";
////    }
////    else
////    {
////        TempData["ErrorMessage"] = "The candidate could not be found.";
////    }
////    return RedirectToAction("ViewCandidates");
////}
//    [HttpPost]
//    public IActionResult DeleteCandidateConfirmed(int id)
//    {
//        var candidate = _context.Candidates.Find(id);
//        if (candidate != null)
//        {
//            _context.Candidates.Remove(candidate);
//            _context.SaveChanges();
//            TempData["SuccessMessage"] = "Candidate deleted successfully.";
//        }
//        else
//        {
//            TempData["ErrorMessage"] = "The candidate could not be found.";
//        }
//        return RedirectToAction("ManageCandidates");
//    }

//    // GET: Edit Candidate Form
//    [HttpGet]
//    public IActionResult EditCandidate(int id)
//    {
//        var candidate = _context.Candidates.Find(id);
//        if (candidate == null)
//        {
//            return NotFound();
//        }
//        return View(candidate);
//    }

//    // POST: Submit Edits to Candidate
//    [HttpPost]
//    public IActionResult EditCandidate(Candidate candidate, IFormFile photo)
//    {
//        if (ModelState.IsValid)
//        {
//            var existingCandidate = _context.Candidates.Find(candidate.Id);
//            if (existingCandidate != null)
//            {
//                // Update candidate details
//                existingCandidate.Name = candidate.Name;
//                existingCandidate.Party = candidate.Party;
//                existingCandidate.Position = candidate.Position;
//                existingCandidate.Description = candidate.Description;

//                // Handle photo upload
//                if (photo != null)
//                {
//                    var photoPath = Path.Combine("wwwroot/uploads", photo.FileName);
//                    using (var stream = new FileStream(photoPath, FileMode.Create))
//                    {
//                        photo.CopyTo(stream);
//                    }
//                    existingCandidate.PhotoPath = "/uploads/" + photo.FileName;
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(ManageCandidates));
//            }
//            else
//            {
//                ModelState.AddModelError("", "Candidate not found.");
//            }
//        }
//        return View(candidate);
//    }
//    public IActionResult ListElections()
//    {
//        var elections = _context.Elections.ToList();
//        return View(elections);
//    }


//    [HttpGet]
//    public IActionResult EditElection(int id)
//    {
//        var election = _context.Elections.FirstOrDefault(e => e.Id == id);
//        if (election == null)
//        {
//            TempData["ErrorMessage"] = "Election not found.";
//            return RedirectToAction("ListElections");
//        }

//        return View(election); // Return election to the edit view
//    }

//    [HttpPost]
//    public IActionResult EditElection(ElectionModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            var election = _context.Elections.FirstOrDefault(e => e.Id == model.Id);
//            if (election != null)
//            {
//                election.Title = model.Title;
//                election.StartDate = model.StartDate;
//                election.EndDate = model.EndDate;
//                _context.SaveChanges();

//                TempData["SuccessMessage"] = "Election updated successfully.";
//            }
//            else
//            {
//                TempData["ErrorMessage"] = "Election not found.";
//            }

//            return RedirectToAction("ListElections");
//        }

//        return View(model); // Show form with validation errors
//    }

//    [HttpGet]
//    public IActionResult DeleteElection(int id)
//    {
//        var election = _context.Elections.FirstOrDefault(e => e.Id == id);
//        if (election != null)
//        {
//            _context.Elections.Remove(election);
//            _context.SaveChanges();
//            TempData["SuccessMessage"] = "Election deleted successfully.";
//        }
//        else
//        {
//            TempData["ErrorMessage"] = "Election not found.";
//        }

//        return RedirectToAction("ListElections");
//    }
//}


//// Edit a candidate
//[HttpGet]
//public IActionResult EditCandidate(int id)
//{
//    var candidate = _context.Candidates.Find(id);
//    if (candidate == null) return NotFound();
//    return View(candidate);
//}

////[HttpPost]
////public IActionResult EditCandidate(Candidate candidate, IFormFile photo)
////{
////    if (ModelState.IsValid)
////    {
////        var existingCandidate = _context.Candidates.Find(candidate.Id);
////        if (existingCandidate != null)
////        {
////            existingCandidate.Name = candidate.Name;
////            existingCandidate.Party = candidate.Party;
////            existingCandidate.Position = candidate.Position;
////            existingCandidate.Description = candidate.Description;

////            if (photo != null)
////            {
////                var photoPath = Path.Combine("wwwroot/uploads", photo.FileName);
////                using (var stream = new FileStream(photoPath, FileMode.Create))
////                {
////                    photo.CopyTo(stream);
////                }
////                existingCandidate.PhotoPath = "/uploads/" + photo.FileName;
////            }

////            _context.SaveChanges();
////            return RedirectToAction(nameof(ManageCandidates));
////        }
////    }
////    return View(candidate);

////}




//public class AdminController : Controller
//{
//    public IActionResult AdminDashBoard() => View();
//    public IActionResult ManageUsers() => View();
//    public IActionResult ManageElections() => View();
//    public IActionResult CountVotes() => View();
//    public IActionResult ManageResults() => View();
//    public IActionResult Details() => View();








//}


























