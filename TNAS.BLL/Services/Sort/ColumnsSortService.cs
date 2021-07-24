using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class ColumnsSortService
    {

        private AppDbContext db;
        public ColumnsSortService(AppDbContext context)
        {
            db = context;
        }
        public async Task<Column> ReduceAfterDeleteAsync(int id)
        {
            var column = db.Columns.FirstOrDefault(x => x.Id == id);
            var result =await db.Columns
                .Where(x => x.BoardId == column.BoardId)
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.SortBy)
                .Skip(column.SortBy+1)
                .ToListAsync();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy--;
            }
            await db.SaveChangesAsync();
            return column;
        }

        public void SwitchColumns(int prevPosition, Column column)
        {
            int currentPosition = column.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = db.Columns
                .Where(x => x.BoardId == column.BoardId)
                .OrderBy(x => x.SortBy)
                .Skip(currentPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                    }
                }
            }
            else
            {
                var result = db.Columns
                .Where(x => x.BoardId == column.BoardId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition + 1)
                .Take(currentPosition - prevPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                }
            }
            db.SaveChanges();
        }
    }
}
