using Election.Db;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ViewElections()
        {
            return View();
        }
        public IActionResult ViewHistory()
        {
            return View();
        }
        public IActionResult ViewResults()
        {
            return View();
        }
        public IActionResult Votersn()
        {
            var voters = _context.ApplicationUser.Where(x => !x.IsDeactivated)
                .Select(v => new ApplicationUserViewModel()
                {
                    Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    DateOfBirth = v.DateOfBirth,
                    Email = v.Email,
                    Gender = v.Gender,
                }).ToList();

            var votersList = voters.Where(v => v.Role.Contains("Voter")).ToList();
            return View(votersList);
        } 
        public IActionResult Voters()
        {
            var voterRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Voter")?.Id;

            if (voterRoleId == null)
            {
                return View(new List<ApplicationUserViewModel>()); // Return an empty list if the role is not found.
            }

            var voters = _context.ApplicationUser
                .Where(x => !x.IsDeactivated &&
                            _context.UserRoles.Any(ur => /*ur.UserId == x.Id*/ ur.RoleId == voterRoleId))
                .Select(v => new ApplicationUserViewModel()
                {
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    DateOfBirth = v.DateOfBirth,
                    Email = v.Email,
                    Gender = v.Gender,
                }).ToList();

            return View(voters);
        }
      


    }


}
