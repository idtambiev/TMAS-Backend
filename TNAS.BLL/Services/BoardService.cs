using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.DAL.DTO;
using AutoMapper;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.Interfaces;
using TMAS.DAL.DTO.View;

namespace TMAS.BLL.Services
{
    public class BoardService : IBoardService
    {
      private readonly  IBoardRepository _boardRepository;
      private readonly IMapper _mapper;
        public BoardService(IBoardRepository repository,IMapper mapper)
        {
            _boardRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoardViewDTO>> GetAll(Guid userId)
        {
            var boards = await _boardRepository.GetAll(userId);
            var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(boards);
            return mapperResult;
        }
        //get boards for Front
        public async Task<BoardViewDTO> GetOne(int boardId)
        {
            if (boardId != null)
            {
                var board = await _boardRepository.GetOne(boardId);
                var mapperResult = _mapper.Map<BoardViewDTO>(board);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty board id");
            }
        }

        //get boards for boardsAccess
        public async Task<Board> GetOneById(int boardId)
        {
            if (boardId != 0)
            {
                var board = await _boardRepository.GetOne(boardId);
                return board;
            }
            else
            {
                throw new Exception("Empty boardId");
            }
        }

        public async Task<IEnumerable<BoardViewDTO>> FindBoard(Guid userId,string search)
        {
            if (search!=null) {
                var boards = await _boardRepository.FindBoard(userId, search);
                var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(boards);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty search text");
            }
        }

        public async Task<BoardViewDTO> Create(string title , Guid id)
        {
            if (title!=null) {
                Board createdBoard = new Board
                {
                    Title = title,
                    BoardUserId = id,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                var result = await _boardRepository.Create(createdBoard);
                var mapperResult = _mapper.Map<BoardViewDTO>(result);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty title");
            }
        }

        public async Task<BoardViewDTO> Update(Board board)
        {
            var result = await _boardRepository.Update(board);
            var mapperResult = _mapper.Map<BoardViewDTO>(result);
            return mapperResult;
        }

        public async Task<BoardViewDTO> Delete(int id)
        {
            if (id!=null) 
            {
                var result = await _boardRepository.Delete(id);
                var mapperResult = _mapper.Map<BoardViewDTO>(result);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty id or title");
            }
        }
    }
}
