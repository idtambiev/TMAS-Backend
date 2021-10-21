using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace TMAS.DAL.Repositories
{
    public class ColumnRepository:IColumnRepository
    {
        private AppDbContext db;
        public ColumnRepository(AppDbContext context)
        {
            db =context;
        }

        public async Task<IEnumerable<Column>> GetAll(int boardId)
        {
            return await db.Columns
                .AsNoTracking()
                .Where(x => x.BoardId == boardId)
                .Where(x => x.IsActive == true)
                .OrderBy(x=>x.SortBy)
                .ToListAsync();
        }

        public async Task<List<Column>> GetAllWithSkip(int boardId, int position)
        {
           var columns=await db.Columns
                .Where(x => x.BoardId == boardId)
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.SortBy)
                .Skip(position)
                .ToListAsync();
            return columns;
        }


        public async Task<Column> GetOne(int columnId)
        {
            var column= await db.Columns.FirstOrDefaultAsync(i => i.Id == columnId);
            return column;
        }

        public async Task<Column> Create(Column column)
        {
            db.Columns.Add(column);
            await db.SaveChangesAsync();
            return column;
        }

        public async Task<Column> Update(Column column)
        {
            db.Columns.Update(column);
            await db.SaveChangesAsync();
            return column;
        }

        public async Task<Column> Delete(int id)
        {
            Column deletedColumn = await db.Columns.FirstOrDefaultAsync(x => x.Id == id);
            if (deletedColumn != null)
            {

                deletedColumn.IsActive = false;
                deletedColumn.UpdatedDate = DateTime.Now;
                await db.SaveChangesAsync();
                return deletedColumn;
            }
            else
            {
                return null;
            }

        }

    }
}
