using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;

namespace LicensePlateCrimeWebApp.Models
{
  public class FirestoreEntity : IFirestoreEntity
  {
    private readonly string dateFormat = "dd/MM/yyyy";
    private readonly string dateTimeFormat = "dd/MM/yyyy - HH:mm";

    [FirestoreDocumentId]
    public string Id { get; set; } = "";

    [FirestoreDocumentCreateTimestamp]
    public DateTime? CreationDate { get; set; }

    [FirestoreDocumentUpdateTimestamp]
    public DateTime? LastUpdateDate { get; set; }

    public string? CreationDateLocalStr => CreationDate?.ToLocalTime().ToString(dateTimeFormat);
    public string? LastUpdateDateLocalStr => LastUpdateDate?.ToLocalTime().ToString(dateTimeFormat);
  }
}
