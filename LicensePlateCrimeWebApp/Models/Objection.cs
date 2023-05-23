using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Models
{
  public class Objection : FirestoreEntity
  {
    [FirestoreProperty]
    public string ViolationId { get; set; }
    public List<ObjectionMessage> Messages { get; set; }
  }
}
