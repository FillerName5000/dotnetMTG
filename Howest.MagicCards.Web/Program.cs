using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories.SqlRepos;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.Web.Components;
using Howest.MagicCards.WebAPI.Configuration;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;


builder.Services.Configure<ApiBehaviourConf>(config.GetSection("ApiSettings"));

builder.Services.AddDbContext<CardsContext>
    (options => options.UseSqlServer(config.GetConnectionString("CardsDb")));
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddAutoMapper(new Type[] { typeof(CardsProfile) });

builder.Services.AddHttpClient("DeckApi", client =>
{
    client.BaseAddress = new Uri(config.GetSection("DeckUri").Value);
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
