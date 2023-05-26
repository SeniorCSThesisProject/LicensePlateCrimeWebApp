using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace LicensePlateCrimeWebApp.Models
{
  [DataContract]
  public class AlertInfo
  {
    [JsonProperty("Date")]
    public DateTime Date { get; set; }
    [JsonProperty("LicensePlate")]
    public string LicensePlate { get; set; }

    [JsonProperty("Location")]
    public string Location { get; set; }

    [JsonProperty("ImageUrl")]
    public string ImageUrl { get; set; }

    //public AlertInfo(string dateIso, string licensePlate, string locationPlusCode, string imageUrl)
    //{
    //  // convert ISO date string to DateTime
    //  Date = DateTime.Parse(dateIso);
    //  LicensePlate = licensePlate;
    //  Location = locationPlusCode;
    //  ImageUrl = imageUrl;
    //}
  }
}
