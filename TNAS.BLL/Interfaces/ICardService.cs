using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.DTO;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardService:IBaseService
    {

        Task<IEnumerable<Card>> FindCard(int id, string search);
        Task<IEnumerable<CardViewDTO>> GetAll(int columnId);
        Task<Card> GetOne(int id);
        Task<Card> Create(Card card);
        Task<Card> Update(Card updatedCard);
        Task<Card> Delete(int id);
    }

}
