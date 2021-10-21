using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Repositories;
using TMAS.DAL.DTO;
using TMAS.DB.Models;
using TMAS.DB.Models.Enums;
using TMAS.DAL.DTO.View;
using TMAS.DAL.DTO.Created;
using FluentValidation;

namespace TMAS.BLL.Services
{
    public class BoardsAccessService:IBoardAccessService
    {
        private readonly IBoardAccessRepository _boardsAccessRepository;
        private readonly IMapper _mapper;
        private readonly IBoardService _boardService;
        private readonly IUserService _userService;
        private readonly IHistoryService _historyService;
        private readonly AbstractValidator<AccessCreatedDTO> _accessValidator;
        public BoardsAccessService(IBoardAccessRepository boardsAccessRepository, IMapper mapper, IBoardService boardService,
            IUserService userService,IHistoryService historyService
            , AbstractValidator<AccessCreatedDTO> accessValidator)
        {
            _boardsAccessRepository = boardsAccessRepository;
            _mapper = mapper;
            _boardService = boardService;
            _userService = userService;
            _historyService = historyService;
            _accessValidator = accessValidator;
        }

        public async Task<AccessCreatedDTO> Create(AccessCreatedDTO access)
        {
            var validationResult = _accessValidator.Validate(access);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var mapperResult = _mapper.Map<AccessCreatedDTO, BoardsAccess>(access);
                var result = await _boardsAccessRepository.Create(mapperResult);
                var user = await _userService.GetOneById(access.UserId);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.AssignUser,
                    access.UserId,
                    user.Name + ' ' + user.LastName,
                    null,
                    null,
                    access.BoardId
                    );

                return access;
            }
        }
        public async Task<IEnumerable<BoardViewDTO>> Get(Guid id)
        {
            var allBoards = await _boardsAccessRepository.Get(id);
            var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(allBoards);
            return mapperResult;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers(int boardId, string text, Guid userId)
        {
            if (text!=null)
            {
                var boards = await _boardService.GetOneById(boardId);
                Guid creatorId = boards.BoardUserId;
                ICollection<UserDTO> users = (await _userService.GetUsers(text, userId, creatorId)).ToList();

                foreach (var user in users.ToList())
                {
                    bool access = await _boardsAccessRepository.CheckAssigningStatus(user.Id, boardId);
                    if (access)
                    {
                        users.Remove(user);
                    }
                }

                return users;
            }
            else
            {
                throw new Exception("Empty id or title");
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAssignedUsers(int boardId,string text,Guid userId)
       {
            if ( text != null)
            {
                var allusers = await _boardsAccessRepository.GetAssignedUsers(boardId, text, userId);
                var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allusers);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty text");
            }

        }

        public async Task<BoardsAccess> Delete(int boardId,Guid userId)
        {

                var user = await _userService.GetOneById(userId);

                var result = await _boardsAccessRepository.Delete(boardId, userId);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.AssignUser,
                    userId,
                    user.Name + ' ' + user.LastName,
                    null,
                    null,
                    boardId
                    );
                return result;

        }
    }
}
