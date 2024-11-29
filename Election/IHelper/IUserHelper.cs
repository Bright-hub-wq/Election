using Election.IHelper;
using Election.Models;
using Election.ViewModel;

namespace Election.IHelper
{
    public interface IUserHelper
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUserViewModel applicationUser);
    }
}
