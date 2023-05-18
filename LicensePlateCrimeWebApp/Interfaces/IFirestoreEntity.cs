using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IFirestoreEntity
  {
    public string Id { get; set; }

    [FirestoreProperty]
    public DateTime? CreationDate { get; set; }

    [FirestoreProperty]
    public DateTime? LastUpdateDate { get; set; }
  }
}
