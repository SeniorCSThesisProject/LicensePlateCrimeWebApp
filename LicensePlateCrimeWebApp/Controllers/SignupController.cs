using Firebase.Auth;
using FireSharp.Extensions;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Controllers
{
	public class SignupController : Controller
	{
		private readonly FirebaseSettings _firebaseSettings;
		private const string EMAIL_EXISTS_ERROR = "Bu e-posta adresi zaten kullanılıyor. Lütfen başka bir e-posta adresi deneyiniz.";
		FirebaseAuthProvider auth;
		public SignupController(IConfiguration configuration)
		{
			_firebaseSettings = configuration.GetSection("Firebase").Get<FirebaseSettings>();
			auth = new FirebaseAuthProvider(
							new FirebaseConfig(_firebaseSettings.WebApiKey));
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registration(LoginModel loginModel)
		{
			try
			{
				//create the user
				await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
				//log in the new user
				var fbAuthLink = await auth
								.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
				string token = fbAuthLink.FirebaseToken;
				//saving the token in a session variable
				//if registration is successful
				if (token != null)
				{
					HttpContext.Session.SetString("_UserToken", token);
					HttpContext.Session.SetString("FbUser", fbAuthLink.User.ToJson());
					return RedirectToAction("Index", "Home", null);
				}
			}
			catch (FirebaseAuthException ex)
			{
				var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
				if (firebaseEx.error != null)
					if (firebaseEx.error.message == "EMAIL_EXISTS")
						ModelState.AddModelError(String.Empty, EMAIL_EXISTS_ERROR);
				return View("Index", loginModel);
			}

			return View("Index", loginModel);

		}

	}
}
