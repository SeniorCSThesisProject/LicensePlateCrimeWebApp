using FirebaseAdmin.Auth;
using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LicensePlateCrimeWebApp.Controllers
{
	public class LoginController : Controller
	{
		private readonly FirebaseAppProvider _firebaseAppProvider;
		private const string LOGIN_ERROR = "E-Posta veya Parola hatalı. Tekrar deneyiniz!";
		public LoginController(FirebaseAppProvider firebaseAppProvider)
		{
			_firebaseAppProvider = firebaseAppProvider;
		}

		// GET: LoginController
		public ActionResult Index(string? returnUrl)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl)
		{
			try
			{
				//log in the user
				var userCredential = await _firebaseAppProvider.FirebaseAuthClient
												.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
				var user = userCredential.User;
				string token = await user.GetIdTokenAsync();
				//saving the token in a session variable
				//if the login is successful
				if (token != null)
				{
					//HttpContext.Session.SetString("_UserToken", token);
					//// convert fbAuthLink.User to json string
					//var jsonUser = JsonConvert.SerializeObject(fbAuthLink.User);
					//HttpContext.Session.SetString("FbUser", jsonUser);

					// Set session expiration to 5 days.
					var options = new SessionCookieOptions()
					{
						ExpiresIn = TimeSpan.FromDays(5),
					};

					// Create the session cookie. This will also verify the ID token in the process.
					// The session cookie will have the same claims as the ID token.
					var sessionCookie = await _firebaseAppProvider.FirebaseAdminAuth
							.CreateSessionCookieAsync(token, options);

					// Set cookie policy parameters as required.
					var cookieOptions = new CookieOptions()
					{
						Expires = DateTimeOffset.UtcNow.Add(options.ExpiresIn),
						HttpOnly = true,
						Secure = true,
					};
					this.Response.Cookies.Append("session", sessionCookie, cookieOptions);

					// convert user to json string
					var userInfoJson = JsonConvert.SerializeObject(user.Info);

					var claims = new List<Claim>
				{
						new Claim("Id", user.Info.Uid),
						new Claim(ClaimTypes.Email, user.Info.Email),
						new Claim("UserInfoJson", userInfoJson)
            //new Claim(ClaimTypes.Role, "Administrator"),
        };
					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var authProperties = new AuthenticationProperties { };

					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity),
							authProperties);

					ViewData["ReturnUrl"] = returnUrl;

					if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
						return LocalRedirect(returnUrl);
					else
						return RedirectToAction("Index", "Home");


					//return RedirectToAction("Index", "Home", null);
				}
				//login failed
				else
				{
					ModelState.AddModelError(String.Empty, LOGIN_ERROR);
					return View("Index", loginModel);
				}
			}
			//login failed?
			catch (Firebase.Auth.FirebaseAuthException e)
			{
				if (e.Reason == Firebase.Auth.AuthErrorReason.WrongPassword)
				{
					ModelState.AddModelError(String.Empty, LOGIN_ERROR);
				}
				return View("Index", loginModel);
			}
		}

		public async Task<IActionResult> Logout()
		{
			if (_firebaseAppProvider.FirebaseAuthClient.User != null)
				_firebaseAppProvider.FirebaseAuthClient.SignOut();
			this.Response.Cookies.Delete("session");
			// Clear the existing external cookie
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index");
		}
	}
}
