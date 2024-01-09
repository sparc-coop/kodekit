//using Kodekit.Web.Pages;
using Kodekit.Features;
using Kodekit.Features.Components;
using Sparc.Authentication.AzureADB2C;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.Sparcify<App>();
builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit");
builder.Services.AddAzureADB2CAuthentication(builder.Configuration);
builder.Services.AddScoped<KitRepository>();

var app = builder.Build();

app.UseCors(policy =>
            policy.WithOrigins("https://localhost:5000", "https://localhost:5001",
                "https://kodekit-test.azurewebsites.net",
                "https://app.kodekit.io")
            .AllowAnyMethod()
            .AllowAnyHeader());

app.Sparcify<App>(app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
    //.AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();