using WebAPIClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

Utilities.Initialize(builder.Configuration);

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
