using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO.View;
using TMAS.DAL.Interfaces;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class CardsSortService:ICardsSortService
    {
        private readonly AppDbContext db;
        private readonly ICardRepository _cardRepository;
        public CardsSortService(AppDbContext context, ICardRepository cardRepository)
        {
            db = context;
            _cardRepository = cardRepository;
        }
        public async Task MoveOnNewColumn(CardViewDTO card)
        {
            int currentPosition = card.SortBy;
            var result = await _cardRepository.GetAllWithSkip(card.ColumnId, currentPosition);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy++;
            }
            await db.SaveChangesAsync();
        }

        public async Task MoveOnOldColumn(Card card)
        {
            int prevPosition = card.SortBy;
            var previousCards = await _cardRepository
                .GetAllWithSkip(card.ColumnId, prevPosition + 1);

            for (int i = 0; i < previousCards.Count; i++)
            {
                previousCards[i].SortBy--;
                await _cardRepository.Update(previousCards[i]);
            }
        }

        public async Task SwitchCards(int prevPosition, CardViewDTO card)
        {
            int currentPosition = card.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = await _cardRepository
                    .GetAllWithSkip(card.ColumnId, currentPosition);

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                        await _cardRepository.Update(result[i]);
                    }
                }
            }
            else
            {
                var result = await _cardRepository
                    .GetAllWithSkip(card.ColumnId, prevPosition+1);

                result = result
                    .Take(currentPosition - prevPosition)
                    .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                    await _cardRepository.Update(result[i]);
                }
            }
            await db.SaveChangesAsync();
        }
        public async Task<Card> ReduceAfterDeleteAsync(int id)
        {
            var card = await _cardRepository.GetOne(id);
            var result = await _cardRepository
                .GetAllWithSkip(card.ColumnId, card.SortBy + 1);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy--;
                await _cardRepository.Update(result[i]);
            }
            return card;
        }

    }
}
