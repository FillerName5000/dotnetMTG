namespace Howest.MagicCards.Shared.DTO
{
    public record CardReadDetailDTO
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string? ManaCost { get; init; }
        public string ConvertedManaCost { get; init; }
        public string Type { get; init; }
        /*public string? RarityCode { get; init; }
        public string SetCode { get; init; }*/ //not needed in this application, only full name instead of the code which is given in Rarity & SetName
        public string? Text { get; init; }
        public string? Flavor { get; init; }
        public string Number { get; init; }
        public string? Power { get; init; }
        public string? Toughness { get; init; }
        public string Layout { get; init; }
        //public int? MultiverseId { get; init; } //not needed in this application
        public string? OriginalImageUrl { get; init; }
        /*public string Image { get; init; }*/
        /*public string? OriginalText { get; init; }
        public string? OriginalType { get; init; }*/ //not needed in this application
        //public string MtgId { get; init; } //only Id within our db is needed here
        //public string? Variations { get; init; } //references other mtgId
        public string ArtistFullName { get; init; }
        public List<string> CardColors { get; init; }
        public List<string> CardTypes { get; init; }
        public string Rarity { get; init; }
        public string SetName { get; init; }
    }
}
