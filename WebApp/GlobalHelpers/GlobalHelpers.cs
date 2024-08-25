using Firebase.Auth;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;

namespace LicensePlateCrimeWebApp.Helpers
{
  public static class GlobalHelpers
  {
    public static bool Authenticated(HttpContext httpContext)
    {
      return httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated;
    }
    public static UserInfo? GetCurrentUserInfo(HttpContext httpContext)
    {
      try
      {
        // get the user claim named UserJson
        var userJson = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserInfoJson")?.Value;
        if (userJson == null)
        {
          return null;
        }
        // convert the userJson to a UserInfo object
        var userInfo = JsonConvert.DeserializeObject<UserInfo>(userJson);

        if (userInfo == null)
        {
          return null;
        }
        return userInfo;
      }
      catch (Exception e)
      {
        throw;
      }
    }
    public static async Task<bool> GetAdminStatusAsync(string id)
    {
      // Check if user is admin
      // Lookup the user associated with the specified uid.
      UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(id);
      object isAdminObj;
      bool isAdmin = false;
      if (userRecord.CustomClaims.TryGetValue("admin", out isAdminObj))
      {
        if ((bool)isAdminObj)
        {
          isAdmin = true;
        }
      }
      return isAdmin;
    }
  }

}
