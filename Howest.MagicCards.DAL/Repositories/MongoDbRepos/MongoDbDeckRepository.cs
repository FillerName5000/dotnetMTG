using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.MongoModels;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.Repositories.MongoDbRepos
{
    public class MongoDbDeckRepository : IDeckRepository
    {
        private readonly DeckContext _db;
        private readonly ILogger<MongoDbDeckRepository> _logger;

        public MongoDbDeckRepository(DeckContext db, ILogger<MongoDbDeckRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Deck CreateDeck(out bool success)
        {
            try
            {
                Deck newDeck = new();
                _db.DecksCollection.InsertOne(newDeck);
                success = true;
                return newDeck;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new deck");
                success = false;
                return new Deck();
            }
        }


        public Deck GetDeck(string id, out bool success)
        {
            Deck foundDeck = FindDeck(id, out _, out success);
            return foundDeck;
        }

        public void AddCardToDeck(string id, string cardName, out bool success)
        {
            try
            {
                Deck deck = FindDeck(id, out FilterDefinition<Deck> filter, out _);
                if (deck == null)
                {
                    _logger.LogWarning($"No deck found with ID {id}. No updates will be made.");
                    success = false;
                    return;
                }
                UpdateDefinition<Deck> update = Builders<Deck>.Update.Inc($"Cards.{cardName}", 1);
                _db.DecksCollection.UpdateOne(filter, update);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(ex, $"An error occurred while updating the deck with ID {id}");
            }
        }

        public void RemoveCardFromDeck(string id, string cardName, out bool success)
        {
            try
            {
                FilterDefinition<Deck> filter = Builders<Deck>.Filter.Eq(d => d.Id, id);
                UpdateDefinition<Deck> update = Builders<Deck>.Update.Inc($"Cards.{cardName}", -1);
                _db.DecksCollection.UpdateOne(filter, update);

                UpdateDefinition<Deck> removeKeyUpdate = Builders<Deck>.Update.Unset($"Cards.{cardName}");
                _db.DecksCollection.UpdateOne(filter & Builders<Deck>.Filter.Lte($"Cards.{cardName}", 0), removeKeyUpdate);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(ex, $"An error occurred while updating the deck with ID {id}");
            }
        }

        private Deck FindDeck(string id, out FilterDefinition<Deck> filter, out bool success)
        {
            filter = null;
            try
            {
                filter = Builders<Deck>.Filter.Eq(d => d.Id, id);
                success = true;
                return _db.DecksCollection.Find(filter).FirstOrDefault();
            }
            catch (ArgumentNullException error)
            {
                _logger.LogError(error, "Deck find error");
                success = false;
                return null;
            }
            catch (FormatException error)
            {
                _logger.LogError(error, "Deck find error");
                success = false;
                return null;
            }
        }
    }
}
