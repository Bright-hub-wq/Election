using Microsoft.AspNetCore.Mvc;

namespace Election.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
