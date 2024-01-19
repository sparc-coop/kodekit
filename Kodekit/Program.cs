using Kodekit;
using Kodekit.Models;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddBlossom("https://localhost:5001");
builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
builder.Services.AddAzureADB2CAuthentication<User>(builder.Configuration);
builder.Services.AddScoped<KitRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddOutputCache();

var app = builder.Build();
app.UseBlossom();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();