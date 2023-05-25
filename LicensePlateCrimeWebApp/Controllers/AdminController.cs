﻿using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using LicensePlateCrimeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  public class AdminController : Controller
  {
    private readonly FirebaseAppProvider _firebaseAppProvider;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IViolationRepository _violationRepository;

    public AdminController(IVehicleRepository vehicleRepository, IContactRepository contactRepository, IViolationRepository violationRepository, FirebaseAppProvider firebaseAppProvider)
    {
      _vehicleRepository = vehicleRepository;
      _contactRepository = contactRepository;
      _violationRepository = violationRepository;
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
        var vehicle = new Vehicle(createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl, createVehicleViewModel.IsWanted);
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
    // GET: Vehicle/Delete/5
    public ActionResult DeleteVehicle(int id)
    {
      return View();
    }
    // POST: Vehicle/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteVehicle(string id)
    {
      var isDeleted = await _vehicleRepository.DeleteAsync(id);
      return RedirectToAction("VehicleIndex", "Admin");
    }
    // GET: Vehicles/Update
    public async Task<ActionResult> UpdateVehicle(string id)
    {
      var vehicle = await _vehicleRepository.GetByIdAsync(id);
      var createVehicleViewModel = new CreateVehicleViewModel
      {
        Id = vehicle.Id,
        OwnerId = vehicle.OwnerId,
        Model = vehicle.Model,
        LicensePlate = vehicle.LicensePlate,
      };
      return View(createVehicleViewModel);
    }
    // POST: Vehicles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> UpdateVehicle(CreateVehicleViewModel createVehicleViewModel)
    {
      if (ModelState.IsValid)
      {
        var uploadedImgUrl = await _vehicleRepository.UploadVehicleImgAsync(createVehicleViewModel.Image);
        var vehicle = new Vehicle(createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl);
        vehicle.Id = createVehicleViewModel.Id;
        //await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.UpdateAsync(vehicle);
        return RedirectToAction("VehicleIndex");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createVehicleViewModel);
      }
    }

    // POST: Vehicle/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SwitchVehicleWanted(string id,bool isWanted)
    {

      var vehicle= await _vehicleRepository.GetByIdAsync(id);
      vehicle.IsWanted = isWanted;
      await _vehicleRepository.UpdateAsync(vehicle);
      return RedirectToAction("VehicleIndex", "Admin");
    }

    //---------------Vehicles---------------//

    //+++++++++++++++Index+++++++++++++++//



    public async Task<ActionResult> Index()
    {
      // Iterate through all users. This will still retrieve users in batches,
      // buffering no more than 1000 users in memory at a time.
      var enumerator = _firebaseAppProvider.FirebaseAdminAuth.ListUsersAsync(null).GetAsyncEnumerator();
      return View(enumerator);
    }
    // GET: Contact/Delete/5
    public ActionResult DeleteUser(int id)
    {
      return View();
    }
    // POST: Contact/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteUser(string id)
    {
      await _firebaseAppProvider.FirebaseAdminAuth.DeleteUserAsync(id);
      return RedirectToAction("Index", "Admin");
      //return View();
    }


    //---------------Index---------------//

    //+++++++++++++++Violation+++++++++++++++//




    public async Task<ActionResult> ViolationIndex()
    {
      var violation = await _violationRepository.GetAllAsync();
      //var vehicles = await _vehicleRepository.GetAllFromUserIdAsync(_firebaseAppProvider.FirebaseAuthClient.User.Uid);
      return View(violation);
    }

    // GET: Violation/Create
    public ActionResult AddViolation()
    {
      var createviolationModel = new Violation();
      return View(createviolationModel);
    }
    // POST: Violation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddViolation(Violation createviolationModel)
    {
      if (ModelState.IsValid)
      {
        var violation = new Violation(createviolationModel.VehicleId, createviolationModel.ViolationType, createviolationModel.Message);
        var id = await _violationRepository.AddAsync(violation);
        violation.Id = id;
        return RedirectToAction("ViolationIndex", "Admin");
      }
      else
      {
        ModelState.AddModelError("", "Failed");
        return View(createviolationModel);
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
      var isDeleted = await _violationRepository.DeleteAsync(id);
      return RedirectToAction("ViolationIndex", "Admin");
    }
    // GET: Violation/Update
    public async Task<ActionResult> UpdateViolation(string id,string vehicleId)
    {
      var violation = await _violationRepository.GetByIdAsync(id);
      var createViolationModel = new Violation()
      {
        Id = violation.Id,
        VehicleId = vehicleId,
        ViolationType = violation.ViolationType,
        Description = violation.Description,
      };
      return View(createViolationModel);
    }
    // POST: Violation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> UpdateViolation(Violation createViolationModel)
    {
      if (ModelState.IsValid)
      {
        var violation = new Violation(createViolationModel.VehicleId, createViolationModel.ViolationType, createViolationModel.Description);
        violation.Id = createViolationModel.Id;
        //await _vehicleRepository.AddAsync(vehicle);
        await _violationRepository.UpdateAsync(violation);
        return RedirectToAction("ViolationIndex","Admin");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createViolationModel);
      }
    }


    //---------------Violation---------------//

    //+++++++++++++++Contact+++++++++++++++//



    public async Task<ActionResult> ContactIndex()
    {
      var contact = await _contactRepository.GetAllAsync();
      return View(contact);

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
      var isDeleted = await _contactRepository.DeleteAsync(id);
      return RedirectToAction("ContactIndex", "Admin");
    }


    //---------------Contact---------------//

    //+++++++++++++++Objection+++++++++++++++//



    public IActionResult ObjectionIndex()
    {
      return View();
    }



    //---------------Objection---------------//

  }
}
