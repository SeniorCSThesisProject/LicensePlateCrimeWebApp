﻿using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Repository
{
  public class VehicleRepository : IVehicleRepository
  {
    private readonly FirestoreProvider _firestoreProvider;

    public VehicleRepository(FirestoreProvider firestoreProvider)
    {
      _firestoreProvider = firestoreProvider;
    }

    public async Task<IEnumerable<Vehicle>> GetAllFromUserIdAsync(string id)
    {
      return await _firestoreProvider.WhereEqualTo<Vehicle>(nameof(Vehicle.OwnerId), id);
    }
    public async Task<string> AddAsync(Vehicle vehicle)
    {
      var id = await _firestoreProvider.AddOrUpdate(vehicle);
      return id;
    }

    public Task<bool> DeleteAsync(string id)
    {
      return _firestoreProvider.Delete<Vehicle>(id);
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
      return await _firestoreProvider.GetAll<Vehicle>();
    }

    public async Task<Vehicle> GetByIdAsync(string id)
    {
      return await _firestoreProvider.Get<Vehicle>(id);
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
      var vehicles = await _firestoreProvider.WhereEqualTo<Vehicle>(nameof(Vehicle.LicensePlate), licensePlate);
      return vehicles.FirstOrDefault();
    }

    public async Task<string> UpdateAsync(Vehicle vehicle)
    {
      var id = await _firestoreProvider.Update(vehicle);
      return id;
    }

    public async Task<string> UploadVehicleImgAsync(IFormFile imgFile)
    {
      return await _firestoreProvider.UploadImageAsync(imgFile);
    }

    public async Task<IEnumerable<Violation>> GetAllViolationsByVehicleIdAsync(string id)
    {
      return await _firestoreProvider.WhereEqualTo<Violation>(nameof(Violation.VehicleId), id);
    }
  }
}
