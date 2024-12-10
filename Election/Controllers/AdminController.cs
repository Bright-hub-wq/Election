using Election.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Election.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashBoard() => View();
        public IActionResult ManageUsers() => View();
        public IActionResult ManageElections() => View();
        public IActionResult CountVotes() => View();
        public IActionResult ManageResults() => View();
        public IActionResult Details() => View();

        






    }


}
