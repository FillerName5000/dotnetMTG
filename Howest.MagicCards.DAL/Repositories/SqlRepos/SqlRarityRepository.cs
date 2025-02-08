using Howest.MagicCards.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.SqlRepos
{
    public class SqlRarityRepository : IRarityRepository
    {
        private readonly CardsContext _db;
        public SqlRarityRepository(CardsContext db)
        {
            _db = db;
        }

        public async Task<Rarity> GetRarityById(long id)
        {
            return await _db.Rarities.Include(r => r.Cards)
                                    .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
