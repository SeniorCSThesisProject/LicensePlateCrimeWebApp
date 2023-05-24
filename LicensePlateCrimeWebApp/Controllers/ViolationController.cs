using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  [Authorize]
  public class ViolationController : Controller
  {
    private readonly IViolationRepository _violationRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly FirebaseAppProvider _firebaseAppProvider;

    public ViolationController(IViolationRepository violationRepository, IVehicleRepository vehicleRepository, FirebaseAppProvider firebaseAppProvider)
    {
      _violationRepository = violationRepository;
      _vehicleRepository = vehicleRepository;
      _firebaseAppProvider = firebaseAppProvider;
    }

    // GET: Violations
    public async Task<ActionResult> Index(string vehicleId)
    {
      IEnumerable<Violation> violations;
      if (!String.IsNullOrEmpty(vehicleId))
      {
        violations = await _vehicleRepository.GetAllViolationsByVehicleIdAsync(vehicleId);
      }
      else
      {
        var user = _firebaseAppProvider.FirebaseAuthClient.User;
        violations = await _violationRepository.GetAllFromUserIdAsync(user.Uid);
      }

      return View(violations);
    }

    // GET: Violations/Details/5
    public async Task<ActionResult> Detail(string id)
    {
      var violation = await _violationRepository.GetByIdAsync(id);
      return View(violation);
    }

    // GET: Violations/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Violations/Create
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

    // GET: Violations/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Violations/Edit/5
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

    // GET: Violations/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Violations/Delete/5
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
