using Q2RazorPage;

var builder = WebApplication.CreateBuilder(args);
//Initialize UrlUtilities with configuration
//DO NOT change this code
Utilities.Initialize(builder.Configuration);
//End
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();
app.Run();
