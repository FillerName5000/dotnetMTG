using Howest.MagicCards.DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace Howest.MagicCards.Shared.Extensions
{
    public static class CardExtensions
    {
        public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards,
            string SetCode, string ArtistName, string Rarity, string Name, string Type, string Text)
        {
            return cards.Where(c => SetCode.IsNullOrEmpty() || c.SetCode.ToLower().Equals(SetCode.ToLower()))
                        .Where(c => ArtistName.IsNullOrEmpty() || c.Artist.FullName.ToLower().Contains(ArtistName.ToLower()))
                        .Where(c => Rarity.IsNullOrEmpty() || c.RarityCodeNavigation.Name.ToLower().Equals(Rarity.ToLower()))
                        .Where(c => Name.IsNullOrEmpty() || c.Name.ToLower().Contains(Name.ToLower()))
                        .Where(c => Type.IsNullOrEmpty() || c.CardTypes.Any(ct => ct.Type.Name.ToLower() == Type.ToLower()))
                        .Where(c => Text.IsNullOrEmpty() || c.Text.ToLower().Contains(Text.ToLower()));
        }

        public static IQueryable<Card> SortByNameAscDesc(this IQueryable<Card> cards, bool? ascending)
        {
            if (!ascending.HasValue)
            {
                return cards;
            }
            else return (bool)ascending ? cards.OrderBy(c => c.Name) : cards.OrderByDescending(c => c.Name);
        }
    }
}
