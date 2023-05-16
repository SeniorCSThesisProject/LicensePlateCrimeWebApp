using LicensePlateCrimeWebApp.Data;
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
    public async Task<bool> AddAsync(Vehicle vehicle)
    {
      return await _firestoreProvider.AddOrUpdate(vehicle);
    }

    public Task<bool> DeleteAsync(string id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
      return await _firestoreProvider.GetAll<Vehicle>();
    }

    public Task<Vehicle> GetByIdAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<Vehicle> GetByIdNoTrackingAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<Vehicle> GetByLicensePlateAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SaveAsync()
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Vehicle vehicle)
    {
      throw new NotImplementedException();
    }

    public async Task<string> UploadVehicleImgAsync(IFormFile imgFile)
    {
      return await _firestoreProvider.UploadImageAsync(imgFile);
    }
  }
}
