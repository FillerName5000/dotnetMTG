using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories.SqlRepos;
using Howest.MagicCards.GraphQL;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<CardsContext>
    (options => options.UseSqlServer(config.GetConnectionString("CardsDb")));
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();
builder.Services.AddScoped<IRarityRepository, SqlRarityRepository>();
builder.Services.AddScoped<ISetRepository, SqlSetRepository>();

builder.Services.AddScoped<RootSchema>();
builder.Services.AddGraphQL()
                .AddGraphTypes(typeof(RootSchema), ServiceLifetime.Scoped)
                .AddDataLoader()
                .AddSystemTextJson();

WebApplication app = builder.Build();

app.UseGraphQL<RootSchema>();
app.UseGraphQLPlayground(
    "/ui/playground",
    new PlaygroundOptions()
    {
        EditorTheme = EditorTheme.Light
    });

app.Run();
