using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Interfaces;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.DAL.Repositories
{
    public class BoardAccessRepository:IBoardAccessRepository
    {
        private readonly AppDbContext db;
 
        public BoardAccessRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task<BoardsAccess> Create(BoardsAccess access)
        {
            var searchResult = await db.BoardsAccesses
                .Where(x => x.BoardId == access.BoardId)
                .Where(x => x.UserId == access.UserId)
                .ToListAsync();


            if (searchResult.Count==0)
            {
                db.BoardsAccesses.Add(access);
                await db.SaveChangesAsync();
                return access;
            }
            else
            {
                return default;
            }
        }

        public async Task<IEnumerable<Board>> Get(Guid id)
        {
            var boards = await db.Boards
                .AsNoTracking()
                .Where(x => x.BoardUserId != id)
                .Include(x => x.BoardsAccesses)
                .Where(a => a.BoardsAccesses.Any(y => y.UserId == id))
                .ToListAsync();
            return boards;     
        }

        public async Task<bool> CheckAssigningStatus(Guid id,int boardId)
        {
            var access = await db.BoardsAccesses
                .FirstOrDefaultAsync(x=>x.BoardId==boardId & x.UserId==id);
            if (access!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAssignedUsers(int id,string text, Guid userId)
        {
            var board = await db.Boards
                .AsNoTracking()
                .Where(x => x.BoardUserId == userId)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            if (board!=null)
            {
                var accesses = await db.Users
                .AsNoTracking()
                .Where(x => x.UserName.Contains(text))
                .Include(x => x.BoardsAccesses)
                .Where(a => a.BoardsAccesses.Any(y => y.BoardId == id))
                .ToListAsync();
                return accesses;
            }else
            {
                return null;
            }
            
        }

        public async Task<BoardsAccess> Delete(int boardId, Guid userId)
        {
            var accesses =await db.BoardsAccesses
                .Where(x => x.BoardId == boardId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            db.BoardsAccesses.Remove(accesses);
            await db.SaveChangesAsync();
            return null;
        }
    }
}
