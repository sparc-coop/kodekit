using Kodekit.Features;
using Sparc.Authentication.AzureADB2C;
using Sparc.Plugins.Database.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.Sparcify<KodekitContext>()
    .AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"], "kodekit-dev")
    .AddAzureADB2CAuthentication(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<>), typeof(CosmosDbRepository<>));
builder.Services.AddScoped<KitRepository>();

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5000", "https://localhost:5001",
        "https://kodekit-test.azurewebsites.net",
        "https://app.kodekit.io")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Sparcify<KodekitContext>(app.Environment);

app.Run();
