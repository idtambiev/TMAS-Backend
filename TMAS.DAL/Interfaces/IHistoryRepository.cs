using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;

namespace TMAS.DAL.Interfaces
{
    public interface IHistoryRepository:IBaseRepository
    {
        Task<IEnumerable<History>> GetAll(int boardId,int skipCount);
        Task<History> GetOne(int id);
        Task<History> Create(History history);
    }
}
