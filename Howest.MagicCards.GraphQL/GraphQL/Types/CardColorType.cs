using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardColorType : ObjectGraphType<CardColor>
    {
        public CardColorType()
        {
            Field(cc => cc.CardId, type: typeof(IntGraphType));
            Field(cc => cc.ColorId, type: typeof(IntGraphType));
        }
    }
}
