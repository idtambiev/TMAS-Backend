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
using TMAS.DB.DTO;
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        private readonly ColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext db;
        private readonly ColumnsSortService _columnsSortService;
        public ColumnService(ColumnRepository repository,IMapper mapper,AppDbContext context,ColumnsSortService columnsSortService)
        {
            _columnRepository = repository;
            _mapper = mapper;
            db = context;
            _columnsSortService = columnsSortService;
        }

        public async Task<IEnumerable<ColumnViewDTO>> GetAll(int boardId)
        {
            var columns= _columnRepository.GetAll(boardId);
            var mapperResult = _mapper.Map<IEnumerable<Column>,IEnumerable<ColumnViewDTO>>(columns);
            return mapperResult;
        }
        public async Task<Column> GetOne(int columnId)
        {
            return await _columnRepository.GetOne(columnId);
        }

        public async Task<Column> Create(ColumnViewDTO column)
        {
            var newColumn = _mapper.Map<ColumnViewDTO,Column>(column);
            return await _columnRepository.Create(newColumn);
        }

        public async Task<Column> Update(Column updatedColumn)
        {
            return _columnRepository.Update(updatedColumn);
        }


        public async Task<Column> Delete(int id)
        {
            var a =await _columnsSortService.ReduceAfterDeleteAsync(id);
            return _columnRepository.Delete(id);
        }

        public async Task<Column> Move(Column movedColumn)
        {
            Column updatedColumn = await db.Columns.FirstOrDefaultAsync(x => x.Id == movedColumn.Id);
            _columnsSortService.SwitchColumns(updatedColumn.SortBy, movedColumn);
            updatedColumn.SortBy = movedColumn.SortBy;
            updatedColumn.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return movedColumn;
        }

        
    }
}
