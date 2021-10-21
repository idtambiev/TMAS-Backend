using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface IBoardRepository:IBaseRepository
    {
        Task<IEnumerable<Board>> GetAll(Guid userId);
        Task<Board> GetOne(int boardId);
        Task<IEnumerable<Board>> FindBoard(Guid id, string search);
        Task<Board> Create(Board board);
        Task<Board> Update(Board board);
        Task<Board> Delete(int id);
    }

}
