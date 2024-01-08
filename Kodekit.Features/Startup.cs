using Sparc.Authentication.AzureADB2C;
using Sparc.Plugins.Database.Cosmos;
using Kodekit.Web.Pages;
using Kodekit.Features.Components;

namespace Kodekit.Features;

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
        services.AddCosmos<KodekitContext>(Configuration["ConnectionStrings:CosmosDb"], "kodekit");
        services.AddAzureADB2CAuthentication(Configuration);
        services.AddScoped<KitRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(policy =>
            policy.WithOrigins("https://localhost:5000", "https://localhost:5001",
                "https://kodekit-test.azurewebsites.net",
                "https://app.kodekit.io")
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.Sparcify<Startup>(env);

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
