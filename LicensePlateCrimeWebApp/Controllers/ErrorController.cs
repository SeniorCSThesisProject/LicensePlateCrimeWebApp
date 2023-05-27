using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  public class ErrorController : Controller
  {
    public IActionResult Error(int code)
    {
      return View();
    }
  }
}
