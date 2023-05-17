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
		public async Task<bool> AddAsync(Contact contact)
		{
			return await _firestoreProvider.AddOrUpdate(contact);
		}

		public Task<bool> DeleteAsync(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Contact>> GetAllAsync()
		{
			return await _firestoreProvider.GetAll<Contact>();
		}

		public Task<Vehicle> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<Vehicle> GetByIdNoTrackingAsync(string id)
		{
			throw new NotImplementedException();
		}



		public Task<bool> SaveAsync()
		{
			throw new NotImplementedException();
		}

		Task<Contact> IContactRepository.GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<Contact> IContactRepository.GetByIdNoTrackingAsync(string id)
		{
			throw new NotImplementedException();
		}
	}
}
