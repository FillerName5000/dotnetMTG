using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.GraphQL.GraphQL.Types
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType(IArtistRepository artistRepository)
        {
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(c => c.Name, type: typeof(StringGraphType));
            Field(c => c.ManaCost, nullable: true, type: typeof(StringGraphType));
            Field(c => c.ConvertedManaCost, type: typeof(StringGraphType));
            Field(c => c.Type, type: typeof(StringGraphType));
            Field(c => c.RarityCode, nullable: true, type: typeof(StringGraphType));
            Field(c => c.SetCode, type: typeof(StringGraphType));
            Field(c => c.Text, nullable: true, type: typeof(StringGraphType));
            Field(c => c.Flavor, nullable: true, type: typeof(StringGraphType));
            Field(c => c.Number, type: typeof(StringGraphType));
            Field(c => c.Power, nullable: true, type: typeof(StringGraphType));
            Field(c => c.Toughness, nullable: true, type: typeof(StringGraphType));
            Field(c => c.Layout, type: typeof(StringGraphType));
            Field(c => c.MultiverseId, nullable: true, type: typeof(IntGraphType));
            Field(c => c.OriginalImageUrl, nullable: true, type: typeof(StringGraphType));
            Field(c => c.Image, type: typeof(StringGraphType));
            Field(c => c.OriginalText, nullable: true, type: typeof(StringGraphType));
            Field(c => c.OriginalType, nullable: true, type: typeof(StringGraphType));
            Field(c => c.MtgId, type: typeof(StringGraphType));
            Field(c => c.Variations, nullable: true, type: typeof(StringGraphType));
            Field<ArtistType>
                ("Artist",
                resolve: context => artistRepository.GetArtistByIdNotAsync(context.Source.ArtistId ?? default)
                );
        }
    }
}
