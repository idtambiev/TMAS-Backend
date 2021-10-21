using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.DAL.Interfaces
{
    public interface IBoardAccessRepository:IBaseRepository
    {
        Task<BoardsAccess> Create(BoardsAccess access);
        Task<IEnumerable<Board>> Get(Guid id);
        Task<IEnumerable<User>> GetAssignedUsers(int id, string text, Guid userId);
        Task<BoardsAccess> Delete(int boardId, Guid userId);
        Task<bool> CheckAssigningStatus(Guid id, int boardId);

    }
}
