using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.DB.DTO;
using AutoMapper;
using TMAS.BLL.Interfaces.BaseInterfaces;

namespace TMAS.BLL.Services
{
    public class BoardService : IBoardService
    {
      private readonly  BoardRepository _boardRepository;
      private readonly IMapper _mapper;
        public BoardService(BoardRepository repository,IMapper mapper)
        {
            _boardRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoardViewDTO>> GetAll(Guid userId)
        {
            var boards = await _boardRepository.GetAll(userId);
            var mapperResult = _mapper.Map<IEnumerable<Board>,IEnumerable<BoardViewDTO>>(boards);
            return mapperResult;
        }
        public async Task<Board> GetOne(int boardId)
        {
             return await _boardRepository.GetOne(boardId);
        }

        public async Task<IEnumerable<Board>> FindBoard(Guid userId,string search)
        {
            return await _boardRepository.FindBoard(userId,search);
        }

        public async Task<Board> Create(string title , Guid id)
        {
            Board createdBoard = new Board
            {
                Title = title,
                BoardUserId = id,
                CreatedDate = DateTime.Now,
                IsActive=true
            };

            return await _boardRepository.Create(createdBoard);
        }

        public async Task<Board> Update(Board board)
        {
            return await _boardRepository.Update(board);
        }

        public async Task<Board> Delete(int id)
        {
            return await _boardRepository.Delete(id);
        }
    }
}
