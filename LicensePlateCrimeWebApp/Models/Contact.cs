using Firebase.Auth;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using LicensePlateCrimeWebApp.Interfaces;
using System.Buffers;
using System.ComponentModel.DataAnnotations;

namespace LicensePlateCrimeWebApp.Models
{
    [FirestoreData]
    public class Contact
    {
        [FirestoreProperty]
				[Required(ErrorMessage = "Ad soyad zorunludur.")]
				public string Name { get; set; }

        [FirestoreProperty]
				[Required(ErrorMessage = "E-Mail zorunludur")]
				[EmailAddress(ErrorMessage = "Hatalı E-Mail!")]
				[MinLength(10, ErrorMessage = "En az 10 karakterden oluşan e-mail giriniz.")]
        public string Email { get; set; }

        [FirestoreProperty]
				[Required(ErrorMessage = "Telefon zorunludur.")]
        public string Telephone { get; set; }

				[FirestoreProperty]
				[Required(ErrorMessage = "Konu zorunludur.")]
				public string Subject { get; set; }

				[FirestoreProperty]
				[Required(ErrorMessage = "Mesaj zorunludur.")]
				[MinLength(5, ErrorMessage = "En az 5 karakterden oluşan mesaj gönderiniz.")]
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
