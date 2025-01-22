using Microsoft.AspNetCore.Mvc;
using Election.Db;
using Election.IHelper;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public IActionResult AdminRegisteration() => View();

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Registeration(ApplicationUserViewModel models)
        {
            if (!ModelState.IsValid) return View(models);

            if (models.Password != models.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords must match.");
                return View(models);
            }

            var userEmail = await _userManager.FindByEmailAsync(models.Email);
            if (userEmail != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(models);
            }

            if (models.Role.Contains("Voter"))
            {
                // Age validation
                var today = DateTime.Today;
                var age = today.Year - models.DateOfBirth.Value.Year;
                if (models.DateOfBirth > today.AddYears(-age)) age--;

                if (age < 18)
                {
                    ModelState.AddModelError("DateOfBirth", "You must be at least 18 years old to register.");
                    return View(models);
                }
            }

            // Create a new user with a unique VoterId
            var user = new ApplicationUser
            {
                UserName = models.Email,
                Email = models.Email,
                VoterId = Guid.NewGuid().ToString() // Automatically generate VoterId
            };

            var result = await _userManager.CreateAsync(user, models.Password);
            if (result.Succeeded)
            {
                if (models.Role.Contains("Voter"))
                {
                    await _userManager.AddToRoleAsync(user, "Voter");
                }

                TempData["Message"] = "Registered Successfully";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(models);
        }



        //// POST: Register
        //[HttpPost]
        //public async Task<IActionResult> Registeration(ApplicationUserViewModel Models)
        //{
        //    if (!ModelState.IsValid) return View(Models);

        //    if (Models.Password != Models.ConfirmPassword)
        //    {
        //        ModelState.AddModelError("ConfirmPassword", "passwords must match.");
        //        return View(Models);
        //    }

        //    var userEmail = await _userManager.FindByEmailAsync(Models.Email);
        //    if (userEmail != null)
        //    {
        //        ModelState.AddModelError("Email", "Email already exists.");
        //        return View(Models);
        //    }
        //    if(Models.Role.Contains("Voter"))
        //    {
        //        // Age validation
        //        var today = DateTime.Today;
        //        var age = today.Year - Models.DateOfBirth.Value.Year;
        //        if (Models.DateOfBirth > today.AddYears(-age)) age--;

        //        if (age < 18)
        //        {
        //            ModelState.AddModelError("DateOfBirth", "You must be at least 18 years old to register.");
        //            return View(Models);
        //        }
        //    }
        //    var addUser = await _userHelper.CreateUserAsync(Models);
        //    if (addUser != null)
        //    {
        //        // Assign the Jobseeker role
        //        //await _userManager.AddToRoleAsync(addUser, "Voter");

        //        TempData["Message"] = "Registered Successfully";
        //        //await _signInManager.PasswordSignInAsync(addUser, Models.Password, true, true);

        //        return RedirectToAction("Login", "Account");
        //    }

        //    return View(Models);
        //}


        // GET: Login
        public IActionResult Login() => View();



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

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false).ConfigureAwait(false);

            if (result.Succeeded)
            {
                // Create claims for the logged-in user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.VoterId ?? string.Empty), // Ensure VoterId is included
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

                // Get user roles and add them as claims
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                // Create claims identity
                var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme); // Use IdentityConstants.ApplicationScheme

                // Create authentication properties
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(12) // Set session duration
                };

                // Sign in the user with the claims identity
                await HttpContext.SignInAsync(
                    IdentityConstants.ApplicationScheme, // Correct scheme for ASP.NET Identity
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Redirect based on roles
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("AdminDashBoard", "Admin");
                }

                if (roles.Contains("Voter"))
                {
                    return RedirectToAction("VoterDashBoard", "Vote");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }





        //// POST: Login
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user == null)
        //    {
        //        TempData["ErrorMessage"] = "Please register first.";
        //        return RedirectToAction("Registeration", "Account");
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false).ConfigureAwait(false);

        //    if (result.Succeeded)
        //    {
        //        var role = await _userManager.GetRolesAsync(user);

        //        if (role.Contains("Admin"))
        //        {
        //            return RedirectToAction("AdminDashBoard", "Admin");
        //        }


        //        if (role.Contains("Voter"))
        //        {
        //            return RedirectToAction("VoterDashBoard", "Vote");
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    return View(model);
        //}

        //// GET: Logout
        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    Response.Cookies.Delete(".AspNetCore.Identity.Application");
        //    return RedirectToAction("Login", "Account");
        //}


        // GET: Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user using the configured authentication scheme
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Redirect to the Login page after signing out
            return RedirectToAction("Login", "Account");
        }




    }

}
