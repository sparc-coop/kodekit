using Kodekit._Plugins.Auth;

BlossomApplication.Run<Program, User>(args,
    builder =>
    {
        //builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
        //builder.Services.AddPasswordless<User>(builder.Configuration);
        //builder.Services.AddScoped<KitRepository>()
        //        .AddScoped<UserRepository>();
    });