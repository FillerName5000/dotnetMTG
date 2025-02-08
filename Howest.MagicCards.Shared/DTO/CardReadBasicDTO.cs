namespace Howest.MagicCards.Shared.DTO
{
    public class CardReadBasicDTO
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string? OriginalImageUrl { get; init; }
        public List<string> CardColors { get; init; }
        public string Rarity { get; init; }
    }
}
