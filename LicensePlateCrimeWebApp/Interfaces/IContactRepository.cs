using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IContactRepository
  {
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact> GetByIdAsync(string id);
    Task<Contact> GetByIdNoTrackingAsync(string id);

    Task<string> AddAsync(Contact contact);
    Task<bool> DeleteAsync(string id);
    Task<bool> SaveAsync();
  }
}
