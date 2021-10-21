using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace TMAS.DAL.Repositories
{
    public class CardRepository:ICardRepository
    {
        private AppDbContext db;

        public CardRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Card>> GetAll(int columnId)
        {
            var result = await db.Cards
                .AsNoTracking()
                .Where(x => x.ColumnId == columnId)
                .Where(x => x.IsActive == true)
                .OrderBy(d => d.SortBy)
                .ToListAsync();
            return result;
        }

        public async Task<List<Card>> GetAllWithSkip(int columnId,int position)
        {
            var result= await db.Cards
                .Where(x => x.ColumnId == columnId)
                .Where(x => x.IsActive == true)
                .OrderBy(d => d.SortBy)
                .Skip(position)
                .ToListAsync();
            return result;
        }

        public async Task<Card> GetOne(int cardId)
        {
            var card = await db.Cards
                .FirstOrDefaultAsync(i => i.Id == cardId);
            return card;
        }

        public async Task<Card> CheckCard(int cardId,bool status)
        {
          var result=  await db.Cards
                .FirstOrDefaultAsync(i => i.Id == cardId);
            result.IsDone = status;
            result.UpdatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return result;
        }
        public async Task<IEnumerable<Card>> FindCards(int columnId, string search)
        {
            var findedCards = await db.Columns
                .Where(x => x.Id == columnId)
                .SelectMany(
                    b => b.Cards
                    .Where(x =>x.Title
                      .Contains(search)))
                .OrderBy(x=>x.SortBy)
                .ToListAsync();
            return findedCards;
        }


        public async Task<Card> Create(Card card)
        {
            db.Cards.Add(card);
            await db.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Update(Card card)
        {
            db.Cards.Update(card);
            await db.SaveChangesAsync();
            return card;
        }
        public async Task<Card> Delete(int id)
        {
            Card deletedCard = await db.Cards.FirstOrDefaultAsync(x => x.Id == id);
            deletedCard.IsActive = false;
            deletedCard.UpdatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return deletedCard;
        }

        public async Task<Card> Move(int id)
        {
            Card deletedCard =await db.Cards.FirstOrDefaultAsync(x => x.Id == id);
            deletedCard.IsActive = false;
            deletedCard.UpdatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return deletedCard;
        }

    }
}
