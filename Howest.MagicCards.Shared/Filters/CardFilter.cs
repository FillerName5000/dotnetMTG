namespace Howest.MagicCards.Shared.Filters
{
    public class CardFilter : PaginationFilter
    {
        public string SetCode { get; set; } = default;
        public string ArtistName { get; set; }
        public string Rarity { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool? Ascending { get; set; }
    }
}