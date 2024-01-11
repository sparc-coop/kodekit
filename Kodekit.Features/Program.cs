using Kodekit.Features;
using Kodekit.Features.Components;
//using Kodekit.Features.Components.Users;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.AddBlossom("https://localhost:5001");
builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
builder.Services.AddAzureADB2CAuthentication<User>(builder.Configuration);
builder.Services.AddScoped<KitRepository>();
builder.Services.AddOutputCache();

var app = builder.Build();
app.UseBlossom();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();