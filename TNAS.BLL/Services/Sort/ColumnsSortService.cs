using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO.View;
using TMAS.DAL.Interfaces;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class ColumnsSortService:IColumnsSortService
    {

        private readonly IColumnRepository _columnRepository;
        public ColumnsSortService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }
        public async Task<Column> ReduceAfterDeleteAsync(int id)
        {
            var column = await _columnRepository.GetOne(id);

            var result = await _columnRepository
                .GetAllWithSkip(column.BoardId, column.SortBy + 1);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy--;
                await _columnRepository.Update(result[i]);
            }
            return column;
        }

        public async Task SwitchColumns(int prevPosition, ColumnViewDTO column)
        {
            int currentPosition = column.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = await _columnRepository
                .GetAllWithSkip(column.BoardId, currentPosition);

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                        await _columnRepository.Update(result[i]);
                    }
                }
            }
            else
            {
                var result = await _columnRepository
                .GetAllWithSkip(column.BoardId, currentPosition);
                result = result.Take(currentPosition - prevPosition).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                    await _columnRepository.Update(result[i]);
                }
            }
        }
    }
}
