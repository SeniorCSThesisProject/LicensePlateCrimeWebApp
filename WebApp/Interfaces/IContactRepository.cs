using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IContactRepository
  {
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<string> AddAsync(Contact contact);
    Task<bool> DeleteAsync(string id);
  }
}
