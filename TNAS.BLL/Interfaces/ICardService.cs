using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardService:IBaseService
    {
        Task<IEnumerable<CardViewDTO>> GetAll(int columnId);
        Task<CardViewDTO> CheckCard(int cardId, bool status, Guid userId);
        Task<CardFullDTO> GetOne(int cardId);
        Task<CardViewDTO> Create(CardCreatedDTO card, Guid userId);
        Task<IEnumerable<CardViewDTO>> FindCard(int columnId, string search);
        Task<CardViewDTO> UpdateTitle(int id, string title, Guid userId);
        Task<CardViewDTO> Move(CardViewDTO movedCard, Guid userId);
        Task<CardViewDTO> MoveOnColumns(CardViewDTO movedCard,Guid userId);
        Task<CardViewDTO> Delete(int id, Guid userId);
        Task<CardViewDTO> UpdateContent(CardContentDTO updatedCard, Guid userId);
    }

}
