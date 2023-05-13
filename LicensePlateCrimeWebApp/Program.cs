using Google.Api;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using LicensePlateCrimeWebApp;
using LicensePlateCrimeWebApp.Data;
using LicensePlateCrimeWebApp.Interfaces;
using LicensePlateCrimeWebApp.Repository;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add sessions
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// Get api secrets
var firebaseSettings =
	builder.Configuration.GetSection("Firebase").Get<FirebaseSettings>();

// Register FirestoreProvider
builder.Services.AddSingleton(_ => new FirestoreProvider(
	new FirestoreDbBuilder
	{
		ProjectId = firebaseSettings.ProjectId,
		JsonCredentials = firebaseSettings.ServiceAccountJson
	}.Build(),
	StorageClient.Create(credential: GoogleCredential.FromJson(firebaseSettings.ServiceAccountJson)),
	firebaseSettings,
	UrlSigner.FromCredentialFile(firebaseSettings.ServiceAccountJsonPath)
));


var app = builder.Build();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
