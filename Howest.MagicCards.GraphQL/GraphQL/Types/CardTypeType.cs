using CardTypeModel = Howest.MagicCards.DAL.Models.CardType;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardTypeType : ObjectGraphType<CardTypeModel>
    {
        public CardTypeType()
        {
            Field(ct => ct.CardId, type: typeof(IntGraphType));
            Field(ct => ct.TypeId, type: typeof(IntGraphType));
        }
    }
}
