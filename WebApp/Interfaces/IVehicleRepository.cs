﻿using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IVehicleRepository
  {
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<IEnumerable<Vehicle>> GetAllFromUserIdAsync(string id);
    Task<Vehicle> GetByIdAsync(string id);
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<Violation>> GetAllViolationsByVehicleIdAsync(string id);
    Task<string> UploadVehicleImgAsync(IFormFile imgFile);
    Task<string> AddAsync(Vehicle vehicle);
    Task<string> UpdateAsync(Vehicle vehicle);
    Task<bool> DeleteAsync(string id);
  }
}
