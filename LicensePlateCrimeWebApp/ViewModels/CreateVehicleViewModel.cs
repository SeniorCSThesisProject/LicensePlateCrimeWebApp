namespace LicensePlateCrimeWebApp.ViewModels
{
  public class CreateVehicleViewModel
  {
    public string? Id { get; set; }
    public string OwnerId { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public IFormFile Image { get; set; }

  }
}
