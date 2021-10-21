using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;
using TMAS.DB.Models.Enums;

namespace TMAS.BLL.Interfaces
{
    public interface IHistoryService:IBaseService
    {
        Task<IEnumerable<HistoryViewDTO>> GetAll(int boardId,int skipCount);
        Task<HistoryViewDTO> Create(History history);
        Task<HistoryViewDTO> CreateHistoryObject(UserActions actionType, Guid userId, string actionObject, int? sourceAction, int? destinationAction, int boardId);
    }
}
