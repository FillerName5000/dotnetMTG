using Howest.MagicCards.MinimalAPI.Extensions;

const string commonPrefix = "/api";

var (builder, services, conf) = WebApplication.CreateBuilder(args);

services.AddServices(conf);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string urlPrefix = conf["ApiPrefix"] ?? commonPrefix;

RouteGroupBuilder deckGroup = app.MapGroup($"{urlPrefix}/deck")
                                 .WithTags("Deck");

deckGroup.MapDeckEndpoints(urlPrefix);

app.Run();

