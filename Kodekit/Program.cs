using Kodekit._Plugins.Blossom;

BlossomApplication.Run<BlossomHtml>(args,
    builder =>
    {
        //builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
        //builder.Services.AddPasswordless<User>(builder.Configuration);
        //builder.Services.AddScoped<KitRepository>()
        //        .AddScoped<UserRepository>();
    });