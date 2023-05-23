using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;

namespace LicensePlateCrimeWebApp.Models
{
  public class Objection : IFirestoreEntity
  {
    public string Id { get; set; } = "";

    [FirestoreProperty]
    public string ViolationId { get; set; }
    public List<ObjectionMessage> Messages { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public DateTime? GetLocalCreationDate => CreationDate?.ToLocalTime();
    public DateTime? GetLocalLastUpdatedDate => LastUpdateDate?.ToLocalTime();
  }
}
