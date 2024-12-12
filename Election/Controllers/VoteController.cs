using Election.Db;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Voters()
        {
            var voters = _context.ApplicationUser.Where(x => !x.IsDeactivated)
                .Select(v => new ApplicationUserViewModel()
                {
                    //Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    DateOfBirth = v.DateOfBirth,
                    Email = v.Email,
                    Gender = v.Gender,
                }).ToList();

            var votersList = voters.Where(v => v.Role.Contains("Voter")).ToList();
            return View(votersList);
        }

        //public IActionResult Voters()
        //{
        //    var voterRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Voter")?.Id;

        //    if (voterRoleId == null)
        //    {
        //        return View(new List<ApplicationUserViewModel>()); // Return an empty list if the role is not found.
        //    }

        //    var voters = _context.ApplicationUser
        //        .Where(x => !x.IsDeactivated &&
        //                    _context.UserRoles.Any(ur => ur.RoleId == voterRoleId))
        //        .Select(v => new ApplicationUserViewModel()
        //        {
        //            Id = v.Id,
        //            FirstName = v.FirstName,
        //            LastName = v.LastName,
        //            DateOfBirth = v.DateOfBirth,
        //            Email = v.Email,
        //            Gender = v.Gender,
        //        }).ToList();

        //    return View(voters);
        //}


        [HttpGet]
        public IActionResult Details(int Id)
        {
            if (Id == 0)
            {
                TempData["ErrorMessage"] = "Invalid voter ID.";
                return RedirectToAction("Index");
            }

            var voter = _context.ApplicationUser
                .Where(x => !x.IsDeactivated && x.Id == Id)
                .Select(v => new ApplicationUserViewModel()
                {
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    Email = v.Email,
                    Gender = v.Gender,
                    DateOfBirth = v.DateOfBirth,
                }).ToList();

            if (voter == null)
            {
                TempData["ErrorMessage"] = "Voter not found.";
                return RedirectToAction("Index");
            }

            return View(voter);
        }

        // GET: Voter/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Voter ID was not provided for deletion.";
                return View("NotFound");
            }

            var voter = await _context.Vote.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if (voter == null)
            {
                TempData["ErrorMessage"] = $"No voter found with ID {id}.";
                return View("NotFound");
            }

            return View(voter);
        }

        // POST: Voter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var voter = await _context.Vote.FindAsync(id);

            if (voter == null)
            {
                TempData["ErrorMessage"] = $"No voter found with ID {id}.";
                return View("NotFound");
            }

            _context.Vote.Remove(voter);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Successfully deleted voter: {voter.FirstName} {voter.LastName}.";
            return RedirectToAction(nameof(Index));
        }



    }


}
