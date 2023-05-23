using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;

namespace LicensePlateCrimeWebApp.Models
{
  public class ObjectionMessage : IFirestoreEntity
  {
    public string Id { get; set; } = "";

    [FirestoreProperty]
    public string ObjectionId { get; set; }

    [FirestoreProperty]
    public string OwnerId { get; set; }

    [FirestoreProperty]
    public string Message { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public DateTime? GetLocalCreationDate => CreationDate?.ToLocalTime();
    public DateTime? GetLocalLastUpdatedDate => LastUpdateDate?.ToLocalTime();
  }
}
