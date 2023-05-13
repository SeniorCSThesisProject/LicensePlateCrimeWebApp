using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
