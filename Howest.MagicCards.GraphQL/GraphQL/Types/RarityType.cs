using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class RarityType : ObjectGraphType<Rarity>
    {
        public RarityType()
        {
            Field(a => a.Id, type: typeof(IdGraphType));
            Field(a => a.Code, type: typeof(StringGraphType));
            Field(a => a.Name, type: typeof(StringGraphType));
            Field(a => a.Cards, type: typeof(StringGraphType));
        }
    }
}
