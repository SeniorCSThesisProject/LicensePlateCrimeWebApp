using Firebase.Auth;
using FireSharp.Extensions;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Controllers
{
	public class SignupController : Controller
	{
		FirebaseAuthProvider auth;
		public SignupController()
		{
			auth = new FirebaseAuthProvider(
							new FirebaseConfig("AIzaSyB3Xey5jQHt7hjgKALgvXwKEA0g9OCw3tk"));
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
					ModelState.AddModelError(String.Empty, firebaseEx.error.message);
				return View("Index", loginModel);
			}

			return View("Index", loginModel);

		}

	}
}
