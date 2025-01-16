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
