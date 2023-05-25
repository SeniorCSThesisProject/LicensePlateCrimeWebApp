namespace LicensePlateCrimeWebApp.Models
{
  public class AlertInfo
  {
    public DateTime Date { get; set; }
    public string LicensePlate { get; set; }
    public string Location { get; set; }
    public string ImageUrl { get; set; }

    public AlertInfo(string dateIso, string licensePlate, string locationPlusCode, string imageUrl)
    {
      // convert ISO date string to DateTime
      Date = DateTime.Parse(dateIso);
      LicensePlate = licensePlate;
      Location = locationPlusCode;
      ImageUrl = imageUrl;
    }
  }
}
