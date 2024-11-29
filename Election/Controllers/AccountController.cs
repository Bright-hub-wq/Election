using Microsoft.AspNetCore.Mvc;
using Election.Db;
using Election.IHelper;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Election.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserHelper _userHelper;

        public AccountController(AppDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IUserHelper userHelper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userHelper = userHelper;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Registeration() => View();
        public IActionResult VerifyPhoneNumber() => View();


        public IActionResult AdminRegisteration() => View();

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Registeration(ApplicationUserViewModel Models)
        {
            if (!ModelState.IsValid) return View(Models);

            if (Models.Password != Models.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "The password and confirm password do not match.");
                return View(Models);
            }

            var userEmail = await _userManager.FindByEmailAsync(Models.Email);
            if (userEmail != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(Models);
            }

            var addUser = await _userHelper.CreateUserAsync(Models);
            if (addUser != null)
            {
                // Assign the Jobseeker role
                //await _userManager.AddToRoleAsync(addUser, "Voter");

                TempData["Message"] = "Registered Successfully";
                await _signInManager.PasswordSignInAsync(addUser, Models.Password, true, true);

                return RedirectToAction("Login", "Account");
            }

            return View(Models);
        }

        // GET: Login
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Please register first.";
                return RedirectToAction("Registeration", "Account");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);

                if (role.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }


                if (role.Contains("Voter"))
                {
                    return RedirectToAction("Index", "Vote");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // GET: Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return RedirectToAction("Login", "Account");
        }
    }
}
