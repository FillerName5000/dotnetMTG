﻿namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        IQueryable<Card> GetAllCards();
        Task<Card> GetCardbyId(long id);
    }
}
