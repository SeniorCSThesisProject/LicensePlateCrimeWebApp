using Firebase.Auth;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using LicensePlateCrimeWebApp.Interfaces;
using System.Buffers;

namespace LicensePlateCrimeWebApp.Models
{
    [FirestoreData]
    public class Contact
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Telephone { get; set; }

		[FirestoreProperty]
		public string Subject { get; set; }

		[FirestoreProperty]
		public string Message { get; set; }

		public Contact(string name, string email, string telephone, string subject, string message)
        {
			Name = name;
			Email = email;
			Telephone = telephone;
			Subject = subject;
			Message = message;
        }

		public Contact()
		{

		}
	}
}
