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
    public interface IColumnService:IBaseService
    {
        Task<IEnumerable<ColumnViewDTO>> GetAll(int boardId);
        Task<ColumnViewDTO> GetOne(int columnId);
        Task<ColumnViewDTO> Create(ColumnViewDTO column, Guid userId);
        Task<ColumnViewDTO> UpdateTitle(int columnId, string newTitle, Guid userId);
        Task<ColumnViewDTO> Delete(int id, Guid userId);
        Task<ColumnViewDTO> Move(ColumnViewDTO movedColumn, Guid userId);
    }
}
