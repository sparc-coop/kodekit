using Kodekit;
using Kodekit.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddBlossom("https://localhost:5001");
//builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);

// Debugging AddCosmos
string connectionString2 = builder.Configuration["ConnectionStrings:CosmosDb"]!;
string databaseName2 = "kodekit";
builder.Services.AddDbContext<KodekitContext>(delegate (DbContextOptionsBuilder options)
{
    options.UseCosmos(connectionString2, databaseName2, delegate (CosmosDbContextOptionsBuilder options)
    {
    });
}, ServiceLifetime.Scoped);
builder.Services.Add(new ServiceDescriptor(typeof(DbContext), typeof(KodekitContext), ServiceLifetime.Scoped));
builder.Services.AddTransient((IServiceProvider sp) => new CosmosDbDatabaseProvider(sp.GetRequiredService<DbContext>(), databaseName2));
//builder.Services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(CosmosDbRepository<>), ServiceLifetime.Scoped));builder.Services.AddAzureADB2CAuthentication<User>(builder.Configuration);

builder.Services.AddScoped<KitRepository>();
builder.Services.AddScoped<ElementRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddOutputCache();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();
//app.UseBlossom();
app.UseAntiforgery();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();