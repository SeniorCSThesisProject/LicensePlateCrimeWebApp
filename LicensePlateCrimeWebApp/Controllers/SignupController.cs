using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
