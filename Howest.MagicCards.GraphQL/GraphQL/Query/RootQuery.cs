using Howest.MagicCards.GraphQL.GraphQL.Types;

namespace Howest.MagicCards.GraphQL
{
    public class RootQuery : ObjectGraphType
    {
        private const int _defaultLimit = 150;

        public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
        {
            Name = "Query";

            Field<ListGraphType<CardType>>(
                "Cards",
                Description = "Get all cards",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> { Name = "limit", Description = "Limit the number of results" }
                },
                resolve: context =>
                {
                    int limit = context.GetArgument<int?>("limit") ?? _defaultLimit;
                    return cardRepository.GetAllCards().Take(limit).ToList();
                }
            );

            Field<ListGraphType<ArtistType>>(
                "Artists",
                Description = "Get all artists",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> { Name = "limit", Description = "Limit the number of results" }
                },
                resolve: context =>
                {
                    int limit = context.GetArgument<int?>("limit") ?? _defaultLimit;
                    return artistRepository.GetAllArtists().Take(limit).ToList();
                }
            );

            Field<ArtistType>(
                "Artist",
                Description = "Get artist by id",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                },
                resolve: context =>
                {
                    int artistId = context.GetArgument<int>("id");
                    return artistRepository.GetArtistById(artistId);
                }
            );
        }
    }
}
