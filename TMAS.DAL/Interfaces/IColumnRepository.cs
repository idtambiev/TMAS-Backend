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
        IEnumerable<Column> GetAll(int boardId);
        Task<Column> GetOne(int id);
        Task<Column> Create(Column column);
        Column Update(Column column);
        Column Delete(int id);
    }
}
