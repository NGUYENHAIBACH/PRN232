using Microsoft.EntityFrameworkCore;
using WebAPIService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var connectionString
    = builder.Configuration.GetConnectionString("MyCnn");
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(NorthwindContext));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI();

app.Run();
    