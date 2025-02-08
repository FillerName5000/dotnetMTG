using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.DAL.Repositories.SqlRepos;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.WebAPI.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.1", new OpenApiInfo
    {
        Title = "Card query API version 1.1",
        Version = "v1.1",
        Description = "Basic API to fetch MTG Cards"
    });
    c.SwaggerDoc("v1.5", new OpenApiInfo
    {
        Title = "Books API version 1.5",
        Version = "v1.5",
        Description = "Basic API to fetch MTG Cards with sorting & individual calls"
    });
});

builder.Services.Configure<ApiBehaviourConf>(config.GetSection("ApiSettings"));

builder.Services.AddDbContext<CardsContext>
    (options => options.UseSqlServer(config.GetConnectionString("CardsDb")));
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddResponseCaching();

builder.Services.AddAutoMapper(new Type[] { typeof(CardsProfile) });

builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 5);
});
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    }
);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Cards API v1.1");
        c.SwaggerEndpoint("/swagger/v1.5/swagger.json", "Cards API v1.5");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();