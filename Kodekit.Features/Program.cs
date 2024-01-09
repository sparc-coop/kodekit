//using Kodekit.Web.Pages;
using Kodekit.Features;
using Kodekit.Features.Components;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.AddBlossom("https://localhost:5001");
builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
builder.Services.AddAzureADB2CAuthentication<User>(builder.Configuration);
builder.Services.AddScoped<KitRepository>();

var app = builder.Build();
app.UseBlossom();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
    //.AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();