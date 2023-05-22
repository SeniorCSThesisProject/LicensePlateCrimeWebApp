using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using LicensePlateCrimeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LicensePlateCrimeWebApp.Controllers
{
  public class AdminController : Controller
  {
    private readonly FirebaseAppProvider _firebaseAppProvider;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IContactRepository _contactRepository;

    public AdminController(IVehicleRepository vehicleRepository, IContactRepository contactRepository, FirebaseAppProvider firebaseAppProvider)
    {
      _vehicleRepository = vehicleRepository;
      _contactRepository = contactRepository;
      _firebaseAppProvider = firebaseAppProvider;
    }



    //+++++++++++++++Vehicles+++++++++++++++//



    // GET: Vehicles
    public async Task<ActionResult> VehicleIndex()
    {
      var vehicles = await _vehicleRepository.GetAllAsync();
      //var vehicles = await _vehicleRepository.GetAllFromUserIdAsync(_firebaseAppProvider.FirebaseAuthClient.User.Uid);
      return View(vehicles);
    }
    // GET: Vehicles/Create
    public ActionResult AddVehicle()
    {
      var currentUser = _firebaseAppProvider.FirebaseAuthClient.User;
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
    public async Task<ActionResult> AddVehicle(CreateVehicleViewModel createVehicleViewModel)
    {
      if (ModelState.IsValid)
      {
        var uploadedImgUrl = await _vehicleRepository.UploadVehicleImgAsync(createVehicleViewModel.Image);
        var vehicle = new Vehicle(createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl);
        var id = await _vehicleRepository.AddAsync(vehicle);
        vehicle.Id = id;
        return RedirectToAction("VehicleIndex");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createVehicleViewModel);
      }
    }
    // GET: Contact/Delete/5
    public ActionResult DeleteVehicle(int id)
    {
      return View();
    }
    // POST: Contact/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteVehicle(string id)
    {
      var isDeleted = await _vehicleRepository.DeleteAsync(id);
      return RedirectToAction("VehicleIndex", "Admin");
    }



    //---------------Vehicles---------------//

    //+++++++++++++++Index+++++++++++++++//



    public IActionResult Index()
    {
      return View();
    }



    //---------------Index---------------//

    //+++++++++++++++Violation+++++++++++++++//




    public IActionResult ViolationIndex()
    {
      return View();
    }

    // GET: Violation/Create
    public ActionResult AddViolation()
    {
      var currentUser = _firebaseAppProvider.FirebaseAuthClient.User;
      var currentUserId = currentUser.Uid;
      var createVehicleViewModel = new CreateVehicleViewModel
      {
        OwnerId = currentUserId
      };
      return View(createVehicleViewModel);
    }
    // POST: Violation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddViolation(CreateVehicleViewModel createVehicleViewModel)
    {
      if (ModelState.IsValid)
      {
        var uploadedImgUrl = await _vehicleRepository.UploadVehicleImgAsync(createVehicleViewModel.Image);
        var vehicle = new Vehicle(createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl);
        var id = await _vehicleRepository.AddAsync(vehicle);
        vehicle.Id = id;
        return RedirectToAction("VehicleIndex");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createVehicleViewModel);
      }
    }
    // GET: Violation/Delete/5
    public ActionResult DeleteViolation(int id)
    {
      return View();
    }
    // POST: Violation/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteViolation(string id)
    {
      var isDeleted = await _vehicleRepository.DeleteAsync(id);
      return RedirectToAction("ViolationIndex", "Admin");
    }



    //---------------Violation---------------//

    //+++++++++++++++Contact+++++++++++++++//



    public IActionResult ContactIndex()
    {
      //var contact = await _contactRepository.GetAllAsync();
      //return View(contact);
      return View();
    }
    // GET: Contact/Delete/5
    public ActionResult DeleteContact(int id)
    {
      return View();
    }
    // POST: Contact/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteContact(string id)
    {
      var isDeleted = await _vehicleRepository.DeleteAsync(id);
      return RedirectToAction("ContactIndex", "Admin");
    }


    //---------------Contact---------------//

    //+++++++++++++++Contest+++++++++++++++//



    public IActionResult ContestIndex()
    {
      return View();
    }



    //---------------Contest---------------//

  }
}
