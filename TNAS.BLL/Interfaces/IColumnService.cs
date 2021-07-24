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
    public interface IColumnService:IBaseService
    {
        Task<IEnumerable<ColumnViewDTO>> GetAll(int boardId);
        Task<Column> GetOne(int id);
        Task<Column> Create(ColumnViewDTO column);
        Task<Column> Update(Column updatedColumn);
        Task<Column> Delete(int id);
    }
}
