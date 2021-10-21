using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using AutoMapper;
using TMAS.DAL.DTO;
using TMAS.DAL.Interfaces;
using TMAS.DB.Models.Enums;
using TMAS.DAL.DTO.View;
using FluentValidation;

namespace TMAS.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<History> _historyValidator;
        public HistoryService(IHistoryRepository repository,IMapper mapper,AbstractValidator<History> historyValidator)
        {
            _historyRepository = repository;
            _mapper = mapper;
            _historyValidator = historyValidator;
        }
        public async Task<IEnumerable<HistoryViewDTO>> GetAll(int boardId,int skipCount)
        {
            var allHistories= await _historyRepository.GetAll(boardId,skipCount);
            var mapperResult = _mapper.Map<IEnumerable<History>,IEnumerable<HistoryViewDTO>>(allHistories);
            return mapperResult;
        }
        public async Task<HistoryViewDTO> CreateHistoryObject(UserActions actionType, Guid userId,string actionObject, int? sourceAction,int? destinationAction,int boardId)
        {
            History newHistory = new History
            {
                ActionType =actionType,
                AuthorId = userId,
                CreatedDate = DateTime.Now,
                ActionObject = actionObject,
                SourceAction = sourceAction,
                DestinationAction = destinationAction,
                BoardId = boardId
            };

            var validationResult = _historyValidator.Validate(newHistory);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var history = await Create(newHistory);
                return history;
            }
        }

        public async Task<HistoryViewDTO> Create(History history)
        {
            var validationResult = _historyValidator.Validate(history);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var createResult = await _historyRepository.Create(history);
                var mapperResult = _mapper.Map<History, HistoryViewDTO>(createResult);
                return mapperResult;
            }
        }

    }
}
