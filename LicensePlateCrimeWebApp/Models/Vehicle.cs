using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Models
{
  [FirestoreData]
  public class Vehicle : FirestoreEntity
  {
    [FirestoreProperty]
    public string OwnerId { get; set; }

    //public Firebase.Auth.User? Owner { get; set; }

    [FirestoreProperty]
    public bool IsWanted { get; set; }

    [FirestoreProperty]
    public string Model { get; set; }

    [FirestoreProperty]
    public string LicensePlate { get; set; }

    [FirestoreProperty]
    public string ImageUrl { get; set; }

    public Vehicle(string ownerId, string model, string licensePlate, string imageUrl, bool isWanted = false)
    {
      OwnerId = ownerId;
      IsWanted = isWanted;
      Model = model;
      LicensePlate = licensePlate;
      ImageUrl = imageUrl;
    }
    public Vehicle()
    {

    }
  }
}
