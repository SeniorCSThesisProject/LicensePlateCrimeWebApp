using LicensePlateCrimeWebApp.Helpers;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using LicensePlateCrimeWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{

  [Authorize]
  public class VehicleController : Controller
  {
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VehicleController(IVehicleRepository vehicleRepository, IHttpContextAccessor httpContextAccessor)
    {
      _vehicleRepository = vehicleRepository;
      _httpContextAccessor = httpContextAccessor;
    }
    // GET: Vehicles
    public async Task<ActionResult> Index()
    {
      var vehicles = await _vehicleRepository.GetAllAsync();
      return View(vehicles);
    }

    // GET: Vehicles/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: Vehicles/Create
    public ActionResult Create()
    {
      var currentUser = GlobalHelpers.GetCurrentUserInfo(_httpContextAccessor.HttpContext);
      var currentUserId = currentUser.Uid;
      var createVehicleViewModel = new CreateVehicleViewModel
      {
        OwnerId = currentUserId
      };
      return View(createVehicleViewModel);
    }

    // POST: Vehicles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateVehicleViewModel createVehicleViewModel)
    {
      if (ModelState.IsValid)
      {
        var uploadedImgUrl = await _vehicleRepository.UploadVehicleImgAsync(createVehicleViewModel.Image);
        var vehicle = new Vehicle(null, createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl);
        await _vehicleRepository.AddAsync(vehicle);
        return RedirectToAction("Index");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createVehicleViewModel);
      }
    }

    // GET: Vehicles/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Vehicles/Edit/5
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

    // GET: Vehicles/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Vehicles/Delete/5
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