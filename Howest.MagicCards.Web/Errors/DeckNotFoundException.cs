namespace Howest.MagicCards.Web.Errors
{
    public class DeckNotFoundException : Exception
    {
        public DeckNotFoundException() : base("Deck not found.")
        {
        }

        public DeckNotFoundException(string message) : base(message)
        {
        }

        public DeckNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
