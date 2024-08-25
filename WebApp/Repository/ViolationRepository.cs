using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Repository
{
  public class ViolationRepository : IViolationRepository
  {
    private readonly FirestoreProvider _firestoreProvider;
    private readonly IVehicleRepository _vehicleRepository;

    public ViolationRepository(FirestoreProvider firestoreProvider, IVehicleRepository vehicleRepository)
    {
      _firestoreProvider = firestoreProvider;
      _vehicleRepository = vehicleRepository;
    }

    public async Task<string> AddAsync(Violation Violation)
    {
      var id = await _firestoreProvider.AddOrUpdate(Violation);
      return id;
    }

    public Task<bool> DeleteAsync(string id)
    {
      return _firestoreProvider.Delete<Violation>(id);
    }

    public async Task<IEnumerable<Violation>> GetAllAsync()
    {
      return await _firestoreProvider.GetAll<Violation>();
    }

    public async Task<IEnumerable<Violation>> GetAllFromUserIdAsync(string id)
    {
      List<Violation> user_violations = new List<Violation>();
      var user_vehicles = await _vehicleRepository.GetAllFromUserIdAsync(id);
      foreach (var vehicle in user_vehicles)
      {
        var violations = await _vehicleRepository.GetAllViolationsByVehicleIdAsync(vehicle.Id);
        user_violations.AddRange(violations);
      }
      return user_violations;
    }

    public async Task<Violation> GetByIdAsync(string id)
    {
      return await _firestoreProvider.Get<Violation>(id);
    }

    public async Task<Vehicle> GetVehicleByIdAsync(string id)
    {
      return await _vehicleRepository.GetByIdAsync(id);
    }

    public async Task<string> UpdateAsync(Violation violation)
    {
      return await _firestoreProvider.Update(violation);
    }

  }
}
