using Kodekit;
using Kodekit.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;
using Sparc.Blossom.Authentication.Passwordless;

var app = BlossomApplication.Run<App, User>(args,
    builder =>
{
    builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
    builder.Services.AddPasswordless<User>(builder.Configuration);
    builder.Services.AddScoped<KitRepository>()
            .AddScoped<UserRepository>();
});

app.Run();