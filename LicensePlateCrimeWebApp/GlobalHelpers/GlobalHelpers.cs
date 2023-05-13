using Firebase.Auth;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Helpers
{
	public static class GlobalHelpers
	{
		public static bool IsUserLoggedIn(HttpContext httpContext)
		{
			return httpContext.Session.GetString("_UserToken") != null;
		}
		public static User GetCurrentUser(HttpContext httpContext)
		{
			try
			{
                var json = httpContext.Session.GetString("FbUser");

				if (json == null)
				{
					throw new NotImplementedException();
				}

                User? user = JsonConvert.DeserializeObject<User>(json);

				if (user == null)
				{
                    throw new NotImplementedException();
                }

				return user;
            }
            catch (Exception e)
			{
				throw;
			}
        }
	}
}
