using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface ICardRepository:IBaseRepository
    {
        Task<IEnumerable<Card>> GetAll(int columnId);
        Task<Card> GetOne(int cardId);
        Task<Card> CheckCard(int cardId, bool status);
        Task<IEnumerable<Card>> FindCards(int columnId, string search);
        Task<Card> Create(Card card);
        Task<Card> Update(Card card);
        //Task<Card> UpdateChanges(Card card);
        Task<List<Card>> GetAllWithSkip(int columnId, int position);
        Task<Card> Delete(int id);
    }
}
