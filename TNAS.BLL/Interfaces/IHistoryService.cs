using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IHistoryService:IBaseService
    {
        Task<IEnumerable<History>> GetAll(Guid userId);
        Task<History> Create(History history, Guid userId);
    }
}
