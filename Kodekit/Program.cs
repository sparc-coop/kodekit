using Kodekit;
using Microsoft.EntityFrameworkCore;
using Sparc.Blossom.Authentication;
using Sparc.Blossom.Passwordless;

BlossomApplication.Run<Html, User>(args,
    builder =>
    {
        builder.Services.AddCosmos<KodekitContext>(builder.Configuration["ConnectionStrings:CosmosDb"]!, "kodekit", ServiceLifetime.Scoped);

        builder.AddBlossomPasswordlessAuthentication<User>();
        // Change default cookie to expire in 30 days
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
        });

        //builder.Services.AddPasswordless<User>(builder.Configuration);
        //builder.Services.AddScoped<KitRepository>()
        //        .AddScoped<UserRepository>();
        
        builder.AddRemoteRepository<GoogleFont, GoogleFontResponse>(
            "https://www.googleapis.com/webfonts/v1/webfonts?key=" + builder.Configuration["GoogleFontsApiKey"],
            x => x.Items.Where(y => y.Category == "serif" || y.Category != "sans-serif"));
    },
    app =>
    {
        app.MapGet("/{kitId1}-{kitId2}.css", async (string kitId1, string kitId2, string? v, string? scope, IRepository<Kit> kits, IRepository<KitRevision> revisions) =>
        {
            var kitId = $"{kitId1}-{kitId2}";
            var kit = await kits.FindAsync(kitId);
            if (kit == null)
                return Results.NotFound();

            var revisionId = v == null ? (kit.PublishedRevisionId ?? kit.Current.Id)
            : v == "live" ? kit.CurrentRevisionId
            : v;

            var revision = revisions.Query(kitId).AsNoTracking().FirstOrDefault(x => x.Id == revisionId);
            if (revision == null)
                return Results.NotFound();

            return Results.Text(revision.Css(app.Environment.ContentRootPath, scope), "text/css");
        });
    });