using Howest.MagicCards.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories.SqlRepos
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly CardsContext _db;
        public SqlArtistRepository(CardsContext db)
        {
            _db = db;
        }

        public IQueryable<Artist> GetAllArtists()
        {
            return _db.Artists.Select(a => a);
        }

        public async Task<Artist> GetArtistById(long id)
        {
            return await _db.Artists.Include(a => a.Cards)
                                    .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Artist GetArtistByIdNotAsync(long id)
        {
            return _db.Artists.Include(a => a.Cards)
                                    .FirstOrDefault(a => a.Id == id);
        }
    }
}
