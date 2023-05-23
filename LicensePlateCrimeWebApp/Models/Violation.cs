using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;

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
	public class Violation : IFirestoreEntity
	{
		public string Id { get; set; } = "";

		[FirestoreProperty]
		public string VehicleId { get; set; }

		[FirestoreProperty]
		public ViolationTypes ViolationType { get; set; }

		[FirestoreProperty]
		public string Message { get; set; }


		[FirestoreProperty]
		public DateTime? CreationDate { get; set; }

		[FirestoreProperty]
		public DateTime? LastUpdateDate { get; set; }

		public DateTime? GetLocalCreationDate => CreationDate?.ToLocalTime();

		public DateTime? GetLocalLastUpdatedDate => LastUpdateDate?.ToLocalTime();

		public Violation(string vehicleId, ViolationTypes vioalationType, string message)
		{
			VehicleId = vehicleId;
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
