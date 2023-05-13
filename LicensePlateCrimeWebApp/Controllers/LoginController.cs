﻿using Firebase.Auth;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly FirebaseSettings _firebaseSettings;
		private const string LOGIN_ERROR = "Giriş yapılamadı. Tekrar deneyiniz!";
        FirebaseAuthProvider auth;
        public LoginController(IConfiguration configuration)
        {
            _firebaseSettings = configuration.GetSection("Firebase").Get<FirebaseSettings>();
			auth = new FirebaseAuthProvider(
							new FirebaseConfig(_firebaseSettings.WebApiKey));
		}

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			try
			{
                //log in the user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                //if the login is successful
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
					// convert fbAuthLink.User to json string
					var jsonUser = JsonConvert.SerializeObject(fbAuthLink.User);
					HttpContext.Session.SetString("FbUser", jsonUser);
                    return RedirectToAction("Index", "Home", null);
                }
                //login failed
                else
                {
                    ModelState.AddModelError(String.Empty, LOGIN_ERROR);
                    return View("Index", loginModel);
                }
            }
            //login failed?
            catch (FirebaseAuthException e)
			{
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(e.ResponseData);
                if (firebaseEx.error != null)
                    if (firebaseEx.error.message == "INVALID_PASSWORD" || firebaseEx.error.message == "EMAIL_NOT_FOUND")
						ModelState.AddModelError(String.Empty, LOGIN_ERROR);
				return View("Index", loginModel);
			}

		}
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("_UserToken");
			return View("Index");
		}
	}
}
