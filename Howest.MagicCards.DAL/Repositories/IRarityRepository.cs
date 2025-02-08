namespace Howest.MagicCards.DAL.Repositories
{
    public interface IRarityRepository
    {
        Task<Rarity> GetRarityById(long id);
    }
}
