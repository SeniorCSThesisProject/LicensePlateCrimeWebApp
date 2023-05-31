using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Models;

namespace LicensePlateCrimeWebApp.Repository
{
  public class ContactRepository : IContactRepository
  {
    private readonly FirestoreProvider _firestoreProvider;

    public ContactRepository(FirestoreProvider firestoreProvider)
    {
      _firestoreProvider = firestoreProvider;
    }
    public async Task<string> AddAsync(Contact contact)
    {
      var id = await _firestoreProvider.AddOrUpdate(contact);
      return id;
    }

    public Task<bool> DeleteAsync(string id)
    {
      return _firestoreProvider.Delete<Contact>(id);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
      return await _firestoreProvider.GetAll<Contact>();
    }
  }
}
