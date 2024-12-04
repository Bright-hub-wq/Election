using Microsoft.AspNetCore.Mvc;

namespace Election.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashBoard()
        {
            return View();
        }
        public IActionResult ManageUsers()
        {
            return View();
        }
        public IActionResult ManageElections()
        {
            return View();
        }
        public IActionResult CountVotes()
        {
            return View();
        }
        public IActionResult ManageResults()
        {
            return View();
        }
    }
}
