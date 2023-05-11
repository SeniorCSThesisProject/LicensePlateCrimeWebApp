namespace LicensePlateCrimeWebApp.Helpers
{
	public static class GlobalHelpers
	{
		public static bool IsUserLoggedIn(HttpContext httpContext)
		{
			return httpContext.Session.GetString("_UserToken") != null;
		}
	}
}
