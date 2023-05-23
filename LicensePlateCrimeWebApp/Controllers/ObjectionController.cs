using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  [Authorize]
  public class ObjectionController : Controller
  {
    // GET: Objections
    public ActionResult Index()
    {
      return View();
    }

    // GET: Objections/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: Objections/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Objections/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Objections/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Objections/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Objections/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Objections/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
