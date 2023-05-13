using Firebase.Auth;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using LicensePlateCrimeWebApp.Interfaces;
using System.Buffers;

namespace LicensePlateCrimeWebApp.Models
{
    [FirestoreData]
    public class Vehicle : IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string OwnerId { get; set; }

        public Firebase.Auth.User? Owner { get; set; }

        [FirestoreProperty]
        public string Model { get; set; }

        [FirestoreProperty]
        public string LicensePlate { get; set; }

        [FirestoreProperty]
        public string ImageUrl { get; set; }

        public Vehicle(User owner, string ownerId, string model, string licensePlate, string imageUrl)
        {
            Owner = owner;
            OwnerId = ownerId;
            Model = model;
            LicensePlate = licensePlate;
            ImageUrl = imageUrl;
        }
        public Vehicle()
        {

        }
    }
}
