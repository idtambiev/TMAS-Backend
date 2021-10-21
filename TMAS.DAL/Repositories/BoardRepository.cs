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
    public class BoardRepository : IBoardRepository
    {
        AppDbContext db;

        public BoardRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Board>> GetAll(Guid userId)
        {
            return await db.Boards
                .AsNoTracking()
                .Where(x=>x.BoardUserId==userId)
                .Where(x=>x.IsActive==true)
                .ToListAsync();
        }
        public async Task<Board> GetOne(int boardId)
        {
            return await db.Boards
                .FirstOrDefaultAsync(x => x.Id == boardId);
        }

        public async Task<IEnumerable<Board>> FindBoard(Guid id,string search)
        {
            return await db.Boards
                .Where(p => p.Title.Contains(search) && p.BoardUserId==id)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<Board> Create(Board board)
        {
           db.Boards.Add(board);
           await db.SaveChangesAsync();
           return board;
        }

        public async Task<Board> Update(Board board)
        {
            Board updatedBoard =await db.Boards.FirstOrDefaultAsync(x => x.Id == board.Id);
            updatedBoard.Title = board.Title;
            updatedBoard.UpdatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return updatedBoard;
        }

        public async Task<Board> Delete(int id)
        {
            Board deletedBoard =await db.Boards.FirstOrDefaultAsync(x => x.Id == id);
            deletedBoard.IsActive = false;
            deletedBoard.UpdatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return deletedBoard;
        }

    }
}
