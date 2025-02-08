using Howest.MagicCards.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.SqlRepos
{
    public class SqlCardRepository : ICardRepository
    {
        private readonly CardsContext _db;

        public SqlCardRepository(CardsContext myCardsDBContext)
        {
            _db = myCardsDBContext;
        }
        public IQueryable<Card> GetAllCards() // IQueryable AKA multiple rows cannot be queried async in the repo, is possible in the controller
        {
            return _db.Cards.Select(c => c);
        }

        public async Task<Card> GetCardbyId(long id)
        {
            return await _db.Cards
                                .Include(c => c.Artist)     //eager loading
                                .Include(c => c.RarityCodeNavigation)
                                .Include(c => c.SetCodeNavigation)
                                .Include(c => c.CardColors).ThenInclude(cc => cc.Color)
                                .Include(c => c.CardTypes).ThenInclude(ct => ct.Type)
                                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}// todo implement generics