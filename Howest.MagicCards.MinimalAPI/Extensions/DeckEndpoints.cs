using Howest.MagicCards.DAL.MongoModels;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.MinimalAPI.Extensions
{
    public static class DeckEndpoints
    {
        public static void MapDeckEndpoints(this RouteGroupBuilder deckGroup, string urlPrefix)
        {
            deckGroup.MapPost("create", (IDeckRepository deckRepo) =>
            {
                Deck deck = deckRepo.CreateDeck(out bool success);
                return success ? Results.Ok(deck) : Results.BadRequest(deck);
            });

            deckGroup.MapGet("{id}", (IDeckRepository deckRepo, string id) =>
            {
                return GetDeck(deckRepo, id);
            });

            deckGroup.MapPost("{id}/{cardName}", (IDeckRepository deckRepo, string id, string cardName) =>
            {
                deckRepo.AddCardToDeck(id, cardName, out bool success);
                if (!success) return Results.BadRequest(new Deck());
                return GetDeck(deckRepo, id);
            });

            deckGroup.MapDelete("{id}/{cardName}", (IDeckRepository deckRepo, string id, string cardName) =>
            {
                deckRepo.RemoveCardFromDeck(id, cardName, out bool success);
                if (!success) return Results.BadRequest(new Deck());
                return GetDeck(deckRepo, id);
            });
        }

        private static IResult GetDeck(IDeckRepository deckRepo, string id)
        {
            Deck deck = deckRepo.GetDeck(id, out bool success);
            return success ? Results.Ok(deck) : Results.BadRequest(deck);
        }
    }
}
