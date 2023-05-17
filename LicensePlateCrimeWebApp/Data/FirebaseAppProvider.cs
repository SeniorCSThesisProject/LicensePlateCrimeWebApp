using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;




namespace LicensePlateCrimeWebApp.Data
{
  public class FirebaseAppProvider
  {
    public FirebaseApp DefaultFirebaseApp { get; private set; } = null!;
    public FirebaseAuth FirebaseAdminAuth { get; private set; } = null!;
    public FirebaseAuthClient FirebaseAuthClient { get; private set; } = null!;

    public FirebaseSettings Settings { get; private set; } = null!;
    public FirebaseAppProvider(FirebaseApp firebaseApp, FirebaseSettings firebaseSettings)
    {
      DefaultFirebaseApp = firebaseApp;
      FirebaseAdminAuth = FirebaseAuth.GetAuth(firebaseApp);
      Settings = firebaseSettings;

      // Configure...
      var config = new FirebaseAuthConfig
      {
        ApiKey = Settings.WebApiKey,
        AuthDomain = Settings.AuthDomain,
        Providers = new FirebaseAuthProvider[]
        {
        // Add and configure individual providers
        new EmailProvider()
            // ...
        },
      };
      // ...and create your FirebaseAuthClient
      FirebaseAuthClient = new FirebaseAuthClient(config);
    }
  }
}