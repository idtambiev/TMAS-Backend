using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class CardsSortService
    {
        private AppDbContext db;
        public CardsSortService(AppDbContext context)
        {
            db = context;
        }
        public void MoveOnNewColumn(Card card)
        {
            int currentPosition = card.SortBy;
            var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x=>x.SortBy)
                .Skip(currentPosition)
                .ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy++;
            }
            db.SaveChanges();
        }

        public void MoveOnOldColumn(Card card)
        {
            int prevPosition = card.SortBy;
            var previousCards = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition+1)
                .ToList();
            for (int i = 0; i < previousCards.Count; i++)
            {
                previousCards[i].SortBy--;
            }
            db.SaveChanges();
        }


        public void SwitchCards(int prevPosition, Card card)
        {
            int currentPosition = card.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(currentPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                    }
                }
            }
            else
            {
                var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition+1)
                .Take(currentPosition - prevPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                }
            }
            db.SaveChanges();
        }
        public async Task<Card> ReduceAfterDeleteAsync(int id)
        {
            var card = db.Cards.FirstOrDefault(x => x.Id == id);
            var result = await db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .Where(x=>x.IsActive==true)
                .OrderBy(x => x.SortBy)
                .Skip(card.SortBy + 1)
                .ToListAsync();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy--;
            }
            await db.SaveChangesAsync();
            return card;
        }

    }
}
