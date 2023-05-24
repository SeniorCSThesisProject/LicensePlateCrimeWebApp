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
		public ActionResult UpdateVehicle()
		{
		
			return View();
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
		public ActionResult UpdateViolation()
		{

			return View();
		}
		// POST: Violation/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> UpdateViolation(Violation createviolationModel)
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
