using Howest.MagicCards.DAL.MongoModels;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IDeckRepository
    {
        Deck CreateDeck(out bool success);
        Deck GetDeck(string id, out bool success);
        void AddCardToDeck(string id, string cardName, out bool success);
        void RemoveCardFromDeck(string id, string cardName, out bool success);
    }
}
