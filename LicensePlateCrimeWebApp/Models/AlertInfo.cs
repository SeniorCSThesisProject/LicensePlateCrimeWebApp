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
  }
}
