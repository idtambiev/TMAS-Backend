using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IBoardService:IBaseService
    {
        Task<IEnumerable<BoardViewDTO>> GetAll(Guid userId);
        Task<BoardViewDTO> GetOne(int boardId);
        Task<Board> GetOneById(int boardId);
        Task<IEnumerable<BoardViewDTO>> FindBoard(Guid userId, string search);
        Task<BoardViewDTO> Create(string title, Guid id);
        Task<BoardViewDTO> Update(Board board);
        Task<BoardViewDTO> Delete(int id);
    }
}
