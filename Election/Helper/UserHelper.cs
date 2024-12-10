using Election.IHelper;
using Election.Models;
using Election.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Election.Helper
{ 
public class UserHelper : IUserHelper
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInrManager;
    public UserHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInrManager = signInManager;
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUserViewModel applicationUser)
    {
        if (applicationUser != null)
        {
            var user = new ApplicationUser();

            user.UserName = applicationUser.Email;
            user.PhoneNumber = applicationUser.PhoneNumber;
            user.Email = applicationUser.Email;
            user.Role = applicationUser.Role;
            user.DateRegistered = DateTime.Now;
            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.DateOfBirth = applicationUser.DateOfBirth; 
            user.IsDeactivated = false;
            user.Gender = applicationUser.Gender;
            user.PhoneNumberConfirmed = false;

            var createdUser = await _userManager.CreateAsync(user, applicationUser.Password).ConfigureAwait(false);

            if (createdUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, applicationUser.Role).ConfigureAwait(false);
                return user;
            }

            return user;
        }

        return null;
    }


}

}

