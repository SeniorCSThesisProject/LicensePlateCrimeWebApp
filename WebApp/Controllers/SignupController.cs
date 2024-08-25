using Firebase.Auth;
using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
	public class SignupController : Controller
	{
		private readonly FirebaseAppProvider _firebaseAppProvider;
		private const string EMAIL_EXISTS_ERROR = "Bu e-posta adresi zaten kullanılıyor. Lütfen başka bir e-posta adresi deneyiniz.";
		public SignupController(FirebaseAppProvider firebaseAppProvider)
		{
			_firebaseAppProvider = firebaseAppProvider;
		}

		public IActionResult Index()
		{
			if (User.Identity != null && User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registration(LoginModel loginModel)
		{
			try
			{
				//create the user
				var userCredential = await _firebaseAppProvider.FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
				var user = userCredential.User;
				var token = await user.GetIdTokenAsync();
				//log in the new user
				if (token != null)
					return RedirectToActionPreserveMethod("Login", "Login", loginModel);
			}
			catch (FirebaseAuthException e)
			{
				if (e.Reason == AuthErrorReason.EmailExists || e.Reason == AuthErrorReason.UnknownEmailAddress)
					ModelState.AddModelError(String.Empty, EMAIL_EXISTS_ERROR);
			}
			return View("Index", loginModel);
		}
	}
}
