using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  [Authorize(Roles = "User")]
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

    // GET: Violations/Detail/5
    public async Task<ActionResult> Detail(string id)
    {
      var violation = await _violationRepository.GetByIdAsync(id);
      return View(violation);
    }
  }
}
