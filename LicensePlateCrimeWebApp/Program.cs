using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using LicensePlateCrimeWebApp;
using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IViolationRepository, ViolationRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



// Get api secrets
var firebaseSettings =
  builder.Configuration.GetSection("Firebase").Get<FirebaseSettings>();

// Register FirebaseAppProvider
builder.Services.AddSingleton(_ => new FirebaseAppProvider(
   FirebaseApp.Create(new AppOptions
   {
     Credential = GoogleCredential.FromJson(firebaseSettings.ServiceAccountJson),
   }), firebaseSettings));

// Register FirestoreProvider
builder.Services.AddSingleton(_ => new FirestoreProvider(
  new FirestoreDbBuilder
  {
    ProjectId = firebaseSettings.ProjectId,
    JsonCredentials = firebaseSettings.ServiceAccountJson
  }.Build(),
  StorageClient.Create(credential: GoogleCredential.FromJson(firebaseSettings.ServiceAccountJson)),
  firebaseSettings
//UrlSigner.FromCredentialFile(firebaseSettings.ServiceAccountJsonPath)
));

// Set up cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.LoginPath = new PathString("/Login/Index");
    options.LogoutPath = new PathString("/Login/Logout");
    options.AccessDeniedPath = new PathString("/Login/AccessDenied");
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.ExpireTimeSpan = TimeSpan.FromSeconds(60);// here 2
  });

// Add sessions
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromSeconds(60);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddLocalization(options =>
{
  options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
  var supportedCultures = new[]
  {
    new CultureInfo("tr-TR"),
    new CultureInfo("en-US")
  };
  options.DefaultRequestCulture = new RequestCulture("tr-TR");
  options.SupportedUICultures = supportedCultures;
  options.SupportedCultures = supportedCultures;
  options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

var app = builder.Build();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
