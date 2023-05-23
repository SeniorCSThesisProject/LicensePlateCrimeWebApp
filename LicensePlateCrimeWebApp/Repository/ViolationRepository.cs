using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Repository
{
  public class ViolationRepository : IViolationRepository
  {
    private readonly FirestoreProvider _firestoreProvider;

    public ViolationRepository(FirestoreProvider firestoreProvider)
    {
      _firestoreProvider = firestoreProvider;
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

    public async Task<Violation> GetByIdAsync(string id)
    {
      return await _firestoreProvider.Get<Violation>(id);
    }

    public Task<Violation> GetByIdNoTrackingAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<Violation> GetByLicensePlateAsync(string id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SaveAsync()
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Violation Violation)
    {
      throw new NotImplementedException();
    }

  }
}
