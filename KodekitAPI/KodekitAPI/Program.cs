using KodekitAPI;
using KodekitAPI.Client.Pages;
using KodekitAPI.Components;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddHttpClient<FigmaService>(client =>
{
    // Configure client if necessary
}).ConfigurePrimaryHttpMessageHandler(handler =>
    new HttpClientHandler()
    {
        // Handler configuration if necessary
    });

builder.Services.AddScoped(provider =>
    new FigmaService(
        provider.GetRequiredService<HttpClient>(),
        "emAOkbS5jC6HcNzfTXVzKZ",
        "https://localhost:44342/figma-callback",
        "files:read,file_comments:write"));

var app = builder.Build();

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
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(KodekitAPI.Client._Imports).Assembly);

app.Run();
