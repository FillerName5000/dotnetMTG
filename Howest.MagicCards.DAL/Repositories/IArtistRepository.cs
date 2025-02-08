namespace Howest.MagicCards.DAL.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistById(long id);
        Artist GetArtistByIdNotAsync(long id);
        IQueryable<Artist> GetAllArtists();
    }
}
