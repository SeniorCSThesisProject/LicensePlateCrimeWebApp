using Firebase.Auth;
using FireSharp.Exceptions;
using FireSharp.Extensions;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class LoginController : Controller
    {
		private const string LOGIN_ERROR = "Giriş yapılamadı. Tekrar deneyiniz!";
        FirebaseAuthProvider auth;
        public LoginController()
        {
			auth = new FirebaseAuthProvider(
							new FirebaseConfig("AIzaSyB3Xey5jQHt7hjgKALgvXwKEA0g9OCw3tk"));
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
                    HttpContext.Session.SetString("FbUser", fbAuthLink.User.ToJson());
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
