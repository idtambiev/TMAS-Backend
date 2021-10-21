using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.BLL.Interfaces.BaseInterfaces;
using AutoMapper;
using TMAS.DAL.DTO;
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;
using TMAS.DAL.Interfaces;
using TMAS.DB.Models.Enums;
using TMAS.DAL.DTO.View;
using FluentValidation;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        private readonly IColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext db;
        private readonly IColumnsSortService _columnsSortService;
        private readonly IHistoryService _historyService;
        private readonly AbstractValidator<ColumnViewDTO> _columnViewValidator;
        public ColumnService(IColumnRepository repository,IMapper mapper,AppDbContext context,IColumnsSortService columnsSortService,
            AbstractValidator<ColumnViewDTO> columnViewValidator,
            IHistoryService historyService)
        {
            _columnRepository = repository;
            _mapper = mapper;
            db = context;
            _columnsSortService = columnsSortService;
            _historyService = historyService;
            _columnViewValidator = columnViewValidator;
        }

        public async Task<IEnumerable<ColumnViewDTO>> GetAll(int boardId)
        {
            if (boardId!=null) {
                var columns = await _columnRepository.GetAll(boardId);
                var mapperResult = _mapper.Map<IEnumerable<Column>, IEnumerable<ColumnViewDTO>>(columns);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty board id");
            }
        }

        public async Task<ColumnViewDTO> GetOne(int columnId)
        {
            if (columnId != null)
            {
                var column = await _columnRepository.GetOne(columnId);
                var mapperResult = _mapper.Map<Column, ColumnViewDTO>(column);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty column id");
            }
        }

        public async Task<ColumnViewDTO> Create(ColumnViewDTO column,Guid userId)
        {
            var validationResult = _columnViewValidator.Validate(column);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var newColumn = _mapper.Map<ColumnViewDTO, Column>(column);
                var createResult = await _columnRepository.Create(newColumn);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.CreateColumn,
                    userId,
                    column.Title,
                    null,
                    null,
                    column.BoardId
                    );
                var mapperResult = _mapper.Map<Column, ColumnViewDTO>(createResult);
                return mapperResult;
            }
        }

        public async Task<ColumnViewDTO> UpdateTitle(int columnId,string newTitle,Guid userId)
        {
            if (columnId!=null && newTitle!=null) {
                Column oldColumn = await _columnRepository.GetOne(columnId);
                oldColumn.Title = newTitle;
                oldColumn.UpdatedDate = DateTime.Now;
                var updateResult = await _columnRepository.Update(oldColumn);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.UpdateCard,
                    userId,
                    newTitle,
                    null,
                    null,
                    oldColumn.BoardId
                    );
                var mapperResult = _mapper.Map<Column, ColumnViewDTO>(updateResult);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty column id or title");
            }
        }


        public async Task<ColumnViewDTO> Delete(int id,Guid userId)
        {
            if (id!=null) {
                var a = await _columnsSortService.ReduceAfterDeleteAsync(id);
                var column = await _columnRepository.Delete(id);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.DeleteColumn,
                    userId,
                    column.Title,
                    null,
                    null,
                    column.BoardId
                    );
                var mapperResult = _mapper.Map<Column, ColumnViewDTO>(column);
                return mapperResult;
            } else
            {
                throw new Exception("Empty board id");
            }

            }

        public async Task<ColumnViewDTO> Move(ColumnViewDTO movedColumn,Guid userId)
        {
            var validationResult = _columnViewValidator.Validate(movedColumn);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                Column updatedColumn = await _columnRepository.GetOne(movedColumn.Id);
                await _columnsSortService.SwitchColumns(updatedColumn.SortBy, movedColumn);
                updatedColumn.SortBy = movedColumn.SortBy;
                updatedColumn.UpdatedDate = DateTime.Now;
                var updateResult = await _columnRepository.Update(updatedColumn);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.MoveColumn,
                    userId,
                    movedColumn.Title,
                    null,
                    null,
                    movedColumn.BoardId
                    );
                var mapperResult = _mapper.Map<Column, ColumnViewDTO>(updateResult);
                return mapperResult;
            }
        }
    }
}
