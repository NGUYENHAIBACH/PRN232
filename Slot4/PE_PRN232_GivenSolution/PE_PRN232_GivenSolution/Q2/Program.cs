using Q2;

var builder = WebApplication.CreateBuilder(args);
//Initialize UrlUtilities with configuration
//DO NOT change this code
Utilities.Initialize(builder.Configuration);
//End
builder.Services.AddControllersWithViews();
var app = builder.Build();


//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();