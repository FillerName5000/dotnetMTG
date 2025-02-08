using Howest.MagicCards.DAL.Config;
using Howest.MagicCards.DAL.MongoModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.DBContext
{
    public class DeckContext
    {
        public readonly IMongoCollection<Deck> DecksCollection;

        public DeckContext(IOptions<DeckStorageDatabaseSettings> bookStoreDatabaseSettings)
        {
            MongoClient mongoClient = new(
                bookStoreDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            DecksCollection = mongoDatabase.GetCollection<Deck>(
                bookStoreDatabaseSettings.Value.DecksCollectionName);
        }
    }
}
