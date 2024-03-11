using Kodekit;
using Kodekit.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Sparc.Blossom;
using Sparc.Blossom.Authentication;
using System.Configuration;
using Sparc.Blossom.Authentication.Passwordless;

var app = BlossomApplication.Run<App, User>(args,
    (services, config) =>
{
    services.AddCosmos<KodekitContext>(config["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
    services.AddPasswordless<User>(config);
    services.AddScoped<KitRepository>()
            .AddScoped<UserRepository>();
});

app.Run();