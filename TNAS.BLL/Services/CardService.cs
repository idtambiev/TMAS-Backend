using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using AutoMapper;
using TMAS.DB.DTO;
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace TMAS.BLL.Services
{
    public class CardService:ICardService
    {
        private readonly CardRepository _cardRepository;
        private readonly IMapper _mapper;
        private AppDbContext db;
        private CardsSortService _sortService;
        public CardService(CardRepository repository,IMapper mapper,AppDbContext context,CardsSortService cardsMoveService)
        {
            _cardRepository = repository;
            _mapper = mapper;
            db = context;
            _sortService = cardsMoveService;
        }
        public async Task<IEnumerable<CardViewDTO>> GetAll(int columnId)
        {
            var allCards = await _cardRepository.GetAll(columnId);
            var mapperResult = _mapper.Map<IEnumerable<Card>,IEnumerable<CardViewDTO>>(allCards);
            return mapperResult;
        }
        public async Task<Card> CheckCard(int cardId,Boolean status)
        {
            return await _cardRepository.CheckCard(cardId,status);
        }

        public async Task<Card> GetOne(int cardId)
        {
            return await _cardRepository.GetOne(cardId);
        }

        public async Task<Card> Create(Card card)
        {
            var newCard = new Card
            {
                Title=card.Title,
                Text = card.Text,
                ColumnId = card.ColumnId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDone=false,
                SortBy=card.SortBy
            };
            return await _cardRepository.Create(newCard);
        }

        public async Task<IEnumerable<Card>> FindCard(int boardId, string search)
        {
            return await _cardRepository.FindCards(boardId,search);
        }
        public async Task<Card> Update(Card updatedCard)
        {
            return await _cardRepository.Update(updatedCard);
        }

        public async Task<Card> Move(Card movedCard)
        {
            Card updatedCard = await db.Cards.FirstOrDefaultAsync(x => x.Id == movedCard.Id);

            _sortService.SwitchCards(updatedCard.SortBy,movedCard);

            updatedCard.SortBy = movedCard.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }

        public async Task<Card> MoveOnColumns(Card movedCard)
        {
            Card updatedCard =await db.Cards.FirstOrDefaultAsync(x => x.Id == movedCard.Id);

            _sortService.MoveOnNewColumn(movedCard);
            _sortService.MoveOnOldColumn(updatedCard);

            updatedCard.ColumnId = movedCard.ColumnId;
            updatedCard.SortBy = movedCard.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }

        public async Task<Card> Delete(int id)
        {
            var reduceResult =await _sortService.ReduceAfterDeleteAsync(id);
            return await _cardRepository.Delete(id);
        }

    }
}
