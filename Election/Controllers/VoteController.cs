using Microsoft.AspNetCore.Mvc;

namespace Election.Controllers
{
    public class VoteController : Controller
    {
        public IActionResult VoterDashboard()
        {
            return View();
        }
        public IActionResult ViewElections()
        {
            return View();
        }
        public IActionResult VoteNow()
        {
            return View();
        }
        public IActionResult ViewResults()
        {
            return View();
        }
    }
}
