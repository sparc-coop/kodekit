using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Kodekit.Features;
using Sparc.Authentication.Blazor;
using System.Threading.Tasks;

namespace Kodekit.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.AddB2CApi<KodekitApi>(
                    "https://kodekitui.onmicrosoft.com/3ad526bb-0eb9-484f-9487-00c739685ad0/Kodekit.API",
                    builder.Configuration["ApiUrl"]);

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
