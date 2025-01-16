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
        public IActionResult ManageResults() => View();
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

        //public async Task<IActionResult> ViewElections()
        //{
        //    var elections = await _context.Elections
        //        .Include(e => e.ElectionCandidates)
        //        .ThenInclude(ec => ec.Candidate)
        //        .ToListAsync();

        //    return View(elections); // Ensure this matches the view's expected model
        //}
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
        public async Task<IActionResult> CreateElection()
        {
            // Fetch candidates from the database
            var candidates = await _context.Candidates.ToListAsync();

            // Map the database entities to the view model
            var candidateModels = candidates.Select(c => new CandidateModel
            {
                Id = c.Id,
                Name = c.Name,
                PhotoPath = c.PhotoPath,
                Party = c.Party
            }).ToList();

            // Pass the mapped candidates to the view model
            var viewModel = new CreateElectionViewModel
            {
                AvailableCandidates = candidateModels
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateElection(CreateElectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload available candidates if the form submission is invalid
                model.AvailableCandidates = await _context.Candidates
                    .Select(c => new CandidateModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Party = c.Party,
                        Position = c.Position,
                        PhotoPath = c.PhotoPath
                    }).ToListAsync();

                return View(model);
            }

            // Create the new election
            var newElection = new ElectionModel
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _context.Elections.Add(newElection);
            await _context.SaveChangesAsync();

            // Assign selected candidates to the election
            if (model.SelectedCandidateIds != null && model.SelectedCandidateIds.Any())
            {
                foreach (var candidateId in model.SelectedCandidateIds)
                {
                    _context.ElectionCandidates.Add(new ElectionCandidate
                    {
                        ElectionId = newElection.Id,
                        CandidateId = candidateId
                    });
                }
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Election created successfully!";
            return RedirectToAction("ViewElections");
        }


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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditElection(ElectionModel model)
        //{
        //    var election = await _context.Elections.FindAsync(model.Id);
        //    if (election == null || DateTime.Now >= election.StartDate)
        //    {
        //        TempData["ErrorMessage"] = "Editing is not allowed for ongoing or ended elections.";
        //        return RedirectToAction("ViewElections");
        //    }

        //    election.Title = model.Title;
        //    election.Description = model.Description;
        //    election.StartDate = model.StartDate;
        //    election.EndDate = model.EndDate;
        //    await _context.SaveChangesAsync();

        //    TempData["SuccessMessage"] = "Election updated successfully!";
        //    return RedirectToAction("ViewElections");
        //}

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







        //[HttpGet]
        //public IActionResult CreateElection()
        //{
        //    return View(); // Show the CreateElection form
        //}

        //[HttpPost]
        //public IActionResult CreateElection(ElectionModel election)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Elections.Add(election); // Save the new election
        //        _context.SaveChanges();

        //        TempData["SuccessMessage"] = "Election created successfully!";
        //        return RedirectToAction("ListElections"); // Redirect to Manage Elections
        //    }

        //    TempData["ErrorMessage"] = "Failed to create election. Please check the details.";
        //    return View(election); // Show the form again with errors
        //}
        [HttpGet]
        public async Task<IActionResult> EditCandidate(int id)
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

                // Ensure the "images" folder exists
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory); // Create the directory if it doesn't exist
                }

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
                    PhotoPath = "/images/" + model.Photo.FileName // Save the path relative to wwwroot
                };

                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                // Use TempData to display a success message
                TempData["SuccessMessage"] = "Candidate created successfully.";

                // Redirect to the candidates list page
                return RedirectToAction("ManageCandidates");
            }

            // If the model is invalid, return the same view with validation errors
            return View(model);
        }




        public async Task<IActionResult> Index()
        {
            var elections = await _context.Elections
                                          .Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
                                          .Include(e => e.ElectionCandidates)
                                          .ThenInclude(ec => ec.Candidate) // Ensure Candidate is included in the ElectionCandidate relationship
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

        [HttpPost]
        public async Task<IActionResult> Vote(int ElectionId, int CandidateId)
        {
            // Add logic to record the vote
            var vote = new Vote
            {
                ElectionId = ElectionId,
                CandidateId = CandidateId,
                VoteDate = DateTime.Now
            };

            _context.Vote.Add(vote);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your vote has been recorded!";
            return RedirectToAction("Index");
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


























