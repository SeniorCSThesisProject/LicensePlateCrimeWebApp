using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LicensePlateCrimeWebApp.Models
{
  [FirestoreData]
  public class Violation : IFirestoreEntity
  {
    public string Id { get; set; } = "";

    [FirestoreProperty]
    public string Model { get; set; }

    [FirestoreProperty]
    public string LicensePlate { get; set; }

    [FirestoreProperty]
    public string ViolationType { get; set; }

    [FirestoreProperty]
    public string Message { get; set; }


    [FirestoreProperty]
    public DateTime? CreationDate { get; set; }

    [FirestoreProperty]
    public DateTime? LastUpdateDate { get; set; }

    public DateTime? GetLocalCreationDate => CreationDate?.ToLocalTime();

    public DateTime? GetLocalLastUpdatedDate => LastUpdateDate?.ToLocalTime();

    public Violation(string model, string licensePlate,string vioalationType,string message)
    {
      Model = model;
      LicensePlate = licensePlate;
      ViolationType = vioalationType;
      Message = message;
      CreationDate = DateTime.UtcNow;
      LastUpdateDate = DateTime.UtcNow;
    }
    public Violation()
    {

    }
  }
}
