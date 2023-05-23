using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Interfaces
{
  public interface IFirestoreEntity
  {
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public DateTime? CreationDate { get; set; }

    [FirestoreProperty]
    public DateTime? LastUpdateDate { get; set; }

    public string? CreationDateLocalStr { get; }
    public string? LastUpdateDateLocalStr { get; }

  }
}
