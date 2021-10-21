using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TMAS.DAL.Repositories
{
    public class HistoryRepository:IHistoryRepository
    {
        private AppDbContext db;

        public HistoryRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<History>> GetAll(int boardId,int skipCount)
        {
            var histories= await db.Histories
                .AsNoTracking()
                .Where(x => x.BoardId == boardId)
                .OrderByDescending(x=>x.CreatedDate)
                .Skip(skipCount*20)
                .Take(20)
                .ToListAsync();
            return histories;
        }

        public async Task<History> GetOne(int id)
        {
            var history= await db.Histories.FirstOrDefaultAsync(i => i.Id == id);
            return history;
        }

        public async Task<History> Create(History history)
        {
            db.Histories.Add(history);
            await db.SaveChangesAsync();
            return history;
        }

    }
}
