using Kodekit;

BlossomApplication.Run<BlossomProgram, User>(args,
    builder =>
    {
        builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);
        
        // Change default cookie to expire in 30 days
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
        });

        //builder.Services.AddPasswordless<User>(builder.Configuration);
        //builder.Services.AddScoped<KitRepository>()
        //        .AddScoped<UserRepository>();
    });