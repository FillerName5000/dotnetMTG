using Howest.MagicCards.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.SqlRepos
{
    public class SqlSetRepository : ISetRepository
    {
        private readonly CardsContext _db;
        public SqlSetRepository(CardsContext db)
        {
            _db = db;
        }

        public async Task<Set> GetSetById(long id)
        {
            return await _db.Sets.Include(s => s.Cards).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
