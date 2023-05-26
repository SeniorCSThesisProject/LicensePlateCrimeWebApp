using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics;

namespace LicensePlateCrimeWebApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHtmlLocalizer<HomeController> _localizer;
    public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
    {
      _logger = logger;
      _localizer = localizer;
    }

    public IActionResult Index()
    {
      ViewBag.Message = TempData["accessDeniedMsg"]?.ToString();
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    #region Localization
    public IActionResult ChangeLanguage(string culture)
    {
      Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
      {
        Expires = DateTimeOffset.UtcNow.AddYears(1)
      });
      return Redirect(Request.Headers["Referer"].ToString());
    }
    #endregion

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}