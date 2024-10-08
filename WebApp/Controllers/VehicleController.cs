﻿using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;
using LicensePlateCrimeWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
  [Authorize(Roles = "User")]
  public class VehicleController : Controller
  {
    private readonly FirebaseAppProvider _firebaseAppProvider;
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleController(IVehicleRepository vehicleRepository, FirebaseAppProvider firebaseAppProvider)
    {
      _vehicleRepository = vehicleRepository;
      _firebaseAppProvider = firebaseAppProvider;
    }
    // GET: Vehicles
    public async Task<ActionResult> Index()
    {
      //var vehicles = await _vehicleRepository.GetAllAsync();
      var vehicles = await _vehicleRepository.GetAllFromUserIdAsync(_firebaseAppProvider.FirebaseAuthClient.User.Uid);
      return View(vehicles);
    }

    // GET: Vehicles/Details/5
    public async Task<ActionResult> Detail(string id)
    {
      var vehicle = await _vehicleRepository.GetByIdAsync(id);
      return View(vehicle);
    }

    // GET: Vehicles/Create
    public ActionResult Create()
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
    public async Task<ActionResult> Create(CreateVehicleViewModel createVehicleViewModel)
    {
      if (ModelState.IsValid)
      {
        var uploadedImgUrl = await _vehicleRepository.UploadVehicleImgAsync(createVehicleViewModel.Image);
        var vehicle = new Vehicle(createVehicleViewModel.OwnerId, createVehicleViewModel.Model, createVehicleViewModel.LicensePlate, uploadedImgUrl);
        var id = await _vehicleRepository.AddAsync(vehicle);
        vehicle.Id = id;
        return RedirectToAction("Index");
      }
      else
      {
        ModelState.AddModelError("", "Photo Upload Failed");
        return View(createVehicleViewModel);
      }
    }

    [AllowAnonymous]
    public ActionResult GetVehicleInfoFromLicensePlate(string licensePlate)
    {
      var vehicle = _vehicleRepository.GetByLicensePlateAsync(licensePlate).Result;
      // return a new json that contains vehicle.Id and vehicle.Model
      return Json(new { id = vehicle?.Id, model = vehicle?.Model });
    }

    // POST: Vehicles/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string id)
    {
      var isDeleted = await _vehicleRepository.DeleteAsync(id);
      return RedirectToAction("Index", "Vehicle");
    }
  }
}