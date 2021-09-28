using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sparc.Authentication.AzureADB2C;
using Sparc.Database.Cosmos;
using Sparc.Features;
using Sparc.Storage.Azure;
using Sparc.Plugins.Database.Cosmos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Blazored.LocalStorage;
using System;

namespace Kodekit.Features
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.Sparcify<Startup>();
            services.AddCosmos<KodekitContext>(Configuration["ConnectionStrings:CosmosDb"], "kodekit-dev");
            //services.BuildServiceProvider().GetRequiredService<KodekitContext>().Database.EnsureCreated();
            //services.AddAzureStorage(Configuration["BlobStorage:ConnectionString"]);
            services.AddAzureADB2CAuthentication(Configuration);
            services.AddBlazoredLocalStorage();           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(policy =>
                policy.WithOrigins("http://localhost:5000", "https://localhost:5001",  
                    "https://kodekit-test.azurewebsites.net",
                    "https://app.kodekit.io")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.Sparcify<Startup>(env);

        }
    }
}
