using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface IColumnRepository:IBaseRepository
    {
        Task<IEnumerable<Column>> GetAll(int boardId);
        Task<Column> GetOne(int columnId);
        Task<Column> Create(Column column);
        Task<Column> Update(Column column);
        Task<Column> Delete(int id);
        Task<List<Column>> GetAllWithSkip(int boardId, int position);
    }
}
