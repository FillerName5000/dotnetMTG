namespace Howest.MagicCards.DAL.Config;

public class DeckStorageDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string DecksCollectionName { get; set; } = null!;
}
