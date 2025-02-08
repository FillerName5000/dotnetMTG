using Howest.MagicCards.DAL.Config;
using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.DAL.Repositories.MongoDbRepos;

namespace Howest.MagicCards.MinimalAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection, ConfigurationManager config)
        {
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
            serviceCollection.Configure<DeckStorageDatabaseSettings>(config.GetSection("MongoDb"));
            serviceCollection.AddSingleton<DeckContext>();
            serviceCollection.AddSingleton<IDeckRepository, MongoDbDeckRepository>();
        }
    }
}
