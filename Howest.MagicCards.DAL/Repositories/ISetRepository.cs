namespace Howest.MagicCards.DAL.Repositories
{
    public interface ISetRepository
    {
        Task<Set> GetSetById(long id);
    }
}
