using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Models
{
  public class ObjectionMessage : FirestoreEntity
  {
    [FirestoreProperty]
    public string ObjectionId { get; set; }

    [FirestoreProperty]
    public string OwnerId { get; set; }

    [FirestoreProperty]
    public string Message { get; set; }
  }
}
