using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kodekit.Features;
using Sparc.Kernel;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazor.Analytics;
using Sparc.Ibis;

namespace Kodekit.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");

        builder.AddB2CApi<KodekitApi>(
                "https://kodekitui.onmicrosoft.com/ccba7246-6276-4566-a964-12d7a2b48198/KodekitAPI.Access",
                builder.Configuration["ApiUrl"]);
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddGoogleAnalytics("UA-69755150-11");
        builder.Services.AddIbis();
       
        var host = builder.Build();
        await host.RunAsync();
    }
}
