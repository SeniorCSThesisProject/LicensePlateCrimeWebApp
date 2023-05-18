using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  public class AdminController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult VehicleIndex()
    {
      return View();
    }
    public IActionResult ViolationIndex()
    {
      return View();
    }
    public IActionResult ContactIndex()
    {
      return View();
    }
    public IActionResult ContestIndex()
    {
      return View();
    }
  }
}
