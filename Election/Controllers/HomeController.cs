using Election.Db;
using Election.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Election.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    //public IActionResult Candidates()
    //{
    //    var candidates = _context.Candidates.ToList(); // Fetch all candidates
    //    return View(candidates);
    //}

    public IActionResult Candidates()
    {
        // Retrieve current election settings (e.g., from database or TempData)
        var currentDate = DateTime.Now;
        var startDate = DateTime.Parse(TempData["StartDate"]?.ToString() ?? DateTime.MinValue.ToString());
        var endDate = DateTime.Parse(TempData["EndDate"]?.ToString() ?? DateTime.MaxValue.ToString());

        if (currentDate < startDate || currentDate > endDate)
        {
            ViewBag.Message = "No active elections at the moment.";
            return View(new List<Candidate>()); // Return an empty list
        }

        var candidates = _context.Candidates.ToList();
        return View(candidates);
    }
    public IActionResult Vote()
    {
        var candidates = _context.Candidates.ToList(); // Fetch all candidates
        return View(candidates);
    }

    [HttpPost]
    public IActionResult SubmitVote(int candidateId)
    {
        var userId = User.Identity.Name; // Assuming users are logged in
        var hasVoted = _context.Vote.Any(v => v.UserId == userId);

        if (hasVoted)
        {
            TempData["Message"] = "You have already voted!";
            return RedirectToAction("Vote");
        }

        // Record the vote
        var vote = new Vote
        {
            CandidateId = candidateId,
            UserId = userId,
            VoteDate = DateTime.Now
        };
        _context.Vote.Add(vote);
        _context.SaveChanges();

        TempData["Message"] = "Your vote has been recorded successfully.";
        return RedirectToAction("Vote");
    }
    public IActionResult Results()
    {
        var results = _context.Candidates
            .Select(c => new
            {
                Candidate = c,
                Votes = _context.Vote.Count(v => v.CandidateId == c.Id)
            })
            .ToList();

        return View(results);
    }



    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
      
    public IActionResult ContactUs()
    {
        return View();
    }  
   
    public IActionResult Services()
    {
        return View();
    }  
    public IActionResult Team()
    {
        return View();
    }



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
