using Google.Cloud.Firestore;

namespace LicensePlateCrimeWebApp.Models
{
  public enum ViolationTypes
  {
    NoLicense,
    NoSeatbelt,
    ImproperLaneSwitching,
    NoInsurance,
    PedestrianPriority,
    PhoneUsage,
    FollowingDistanceTooLow,
    ObligatoryRepairDateDue,
    NoParkingInDisabledSpot,
    EmergencyLine,
    MtvDebtDue
  }



  [FirestoreData]
  public class Violation : FirestoreEntity
  {
    public Dictionary<ViolationTypes, string> ViolationTypeDescriptions = new()
    {
      { ViolationTypes.NoLicense, "Ehliyetsiz araç kullanma" },
      { ViolationTypes.NoSeatbelt, "Emniyet kemeri takmama" },
      { ViolationTypes.ImproperLaneSwitching, "Şerit izleme-değiştirme kurallarına uymama" },
      { ViolationTypes.NoInsurance, "Trafik sigortası olmadan trafiğe çıkma" },
      { ViolationTypes.PedestrianPriority, "Yayaya  öncelik vermeme cezası" },
      { ViolationTypes.PhoneUsage, "Seyir halinde cep telefonu kullanma" },
      { ViolationTypes.FollowingDistanceTooLow, "Takip mesafesine uymama" },
      { ViolationTypes.ObligatoryRepairDateDue, "Aracın Zorunlu Muayenesi geçmiştir" },
      { ViolationTypes.NoParkingInDisabledSpot, "Engelli için ayrılan yere park etme" },
      { ViolationTypes.EmergencyLine, "Emniyet şeridini ihlal etme" },
      { ViolationTypes.MtvDebtDue, "MTV borcu bulunmaktadır" }
    };

    [FirestoreProperty]
    public string VehicleId { get; set; }

    [FirestoreProperty]
    public ViolationTypes ViolationType { get; set; }

    [FirestoreProperty]
    public string Description { get; set; } = "";

    [FirestoreProperty]
    public string Message { get; set; }
    public Violation(string vehicleId, ViolationTypes vioalationType, string message)
    {
      VehicleId = vehicleId;
      ViolationType = vioalationType;
      Description = ViolationTypeDescriptions[vioalationType];
      Message = message;
    }
    public Violation()
    {

    }
    public void RefreshDescription()
    {
      Description = ViolationTypeDescriptions[ViolationType];
    }
  }
}
