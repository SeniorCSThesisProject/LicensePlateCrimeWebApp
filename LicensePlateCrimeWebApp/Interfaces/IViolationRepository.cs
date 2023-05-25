using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IViolationRepository
  {
    Task<IEnumerable<Violation>> GetAllAsync();
    Task<IEnumerable<Violation>> GetAllFromUserIdAsync(string id);
    Task<Vehicle> GetVehicleByIdAsync(string id);
    Task<Violation> GetByIdAsync(string id);
    Task<Violation> GetByIdNoTrackingAsync(string id);
    Task<Violation> GetByLicensePlateAsync(string id);

    Task<string> AddAsync(Violation violation);
    Task<string> UpdateAsync(Violation violation);
    Task<bool> DeleteAsync(string id);
    Task<bool> SaveAsync();
  }
}
