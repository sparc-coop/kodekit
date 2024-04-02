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
    builder.Services.AddCosmos<KodekitContext>(config["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
    builder.Services.AddPasswordless<User>(config);
    builder.Services.AddScoped<KitRepository>()
            .AddScoped<UserRepository>();
});

app.Run();