using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class ArtistType : ObjectGraphType<Artist>
    {
        public ArtistType()
        {
            Field(a => a.Id, type: typeof(IdGraphType));
            Field(a => a.FullName, type: typeof(StringGraphType));
            Field(a => a.Cards, type: typeof(StringGraphType));
        }
    }
}
