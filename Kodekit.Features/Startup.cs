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
            services.AddAzureStorage(Configuration["BlobStorage:ConnectionString"]);
            services.AddAzureADB2CAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Sparcify<Startup>(env);
        }
    }
}
