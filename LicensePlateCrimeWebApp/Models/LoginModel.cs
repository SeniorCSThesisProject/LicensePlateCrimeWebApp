using System.ComponentModel.DataAnnotations;

namespace LicensePlateCrimeWebApp.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "E-Mail zorunludur")]
    [EmailAddress(ErrorMessage = "Hatalı E-Mail!")]
    [MinLength(10, ErrorMessage = "En az 10 karakterden oluşan e-mail giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur")]
    [MinLength(3, ErrorMessage = "En az 3 karakterden oluşan şifre giriniz.")]
    public string Password { get; set; }
  }
}
