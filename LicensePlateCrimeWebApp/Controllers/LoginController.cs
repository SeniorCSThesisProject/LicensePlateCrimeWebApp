using Firebase.Auth;
using LicensePlateCrimeWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlateCrimeWebApp.Controllers
{
    public class LoginController : Controller
    {
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
			//log in the user
			var fbAuthLink = await auth
							.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
			string token = fbAuthLink.FirebaseToken;
			//saving the token in a session variable
			//if the login is successful
			if (token != null)
			{
				HttpContext.Session.SetString("_UserToken", token);

				return RedirectToAction("Index", "Home", null);
			}
			//login failed
			else
			{
				return View();
			}
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("_UserToken");
			return RedirectToAction("Index");
		}
	}
}
