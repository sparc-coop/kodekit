using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kodekit.Features;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazor.Analytics;
using Sparc.Blossom;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.RootComponents.Add<App>("app");

builder.AddB2CApi<KodekitApi>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddGoogleAnalytics("UA-69755150-11");

var host = builder.Build();
await host.RunAsync();