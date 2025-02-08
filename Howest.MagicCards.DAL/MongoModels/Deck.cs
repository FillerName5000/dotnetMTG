using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Howest.MagicCards.DAL.MongoModels
{
    public class Deck
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cards")]// mongodb convention is camelCase 
        public Dictionary<string, int> Cards { get; set; } = [];
    }
}
